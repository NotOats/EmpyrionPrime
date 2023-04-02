using Eleon;
using Eleon.Modding;
using EmpyrionPrime.Plugin.Api;
using EmpyrionPrime.RemoteClient.Epm.Api;
using EmpyrionPrime.RemoteClient.Epm.Serializers;
using EmpyrionPrime.RemoteClient.Streams;
using Microsoft.Extensions.Logging;
using Nito.AsyncEx;
using Nito.AsyncEx.Synchronous;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace EmpyrionPrime.RemoteClient.Epm
{
    public class EpmClientAsync : IRemoteEmpyrion, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IPEndPoint _endPoint;

        private readonly CancellationTokenSource _shutdownCts = new CancellationTokenSource();
        private readonly AsyncManualResetEvent _writerNotifier = new AsyncManualResetEvent(false);
        private readonly ConcurrentQueue<GameEvent> _writerQueue = new ConcurrentQueue<GameEvent>();

        private readonly object _taskLock = new object();
        private Task _readerTask;
        private Task _writerTask;

        private readonly SemaphoreSlim _tcpClientLock = new SemaphoreSlim(1, 1);
        private TcpClient _tcpClient = null;
        private AsyncBinaryReader _reader = null;
        private AsyncBinaryWriter _writer = null;

        private int _disposeCount = 0;

        public int ClientId { get; }

        public event Action OnConnected;
        public event Action OnDisconnected;
        public event Action<GameEvent> GameEventHandler;

        public EpmClientAsync(ILogger logger, string ipString, int port, int clientId = -1)
        {
            if (!IPAddress.TryParse(ipString, out IPAddress address))
                throw new ArgumentException("Invalid IP address", nameof(ipString));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _endPoint = new IPEndPoint(address, port);

            ClientId = clientId == -1 ? Process.GetCurrentProcess().Id : clientId;
        }

        public void Dispose()
        {
            if (Interlocked.Increment(ref _disposeCount) != 1)
                return;

            _shutdownCts.Cancel();

            try
            {
                var finished = Task.WaitAll(new[] { _readerTask, _writerTask }, 10 * 1000);
                if (!finished)
                {
                    _logger.LogError("Reader/Writer tasks failed to stop in time.");
                }
            }
            catch(AggregateException ex)
            {
                if (ex.InnerException.GetType() == typeof(TaskCanceledException))
                    return;

                throw;
            }

            CloseConnectionAsync(CancellationToken.None).WaitAndUnwrapException();
        }

        public void Start()
        {
            ThrowIfDisposed();

            lock (_taskLock)
            {
                if (_readerTask == null)
                    _readerTask = Task.Run(async () => { await ReadSocketAsync(_shutdownCts.Token); });

                if (_writerTask == null)
                    _writerTask = Task.Run(async () => { await WriteSocketAsync(_shutdownCts.Token); });
            }
        }

        public void SendRequest(CmdId id, ushort sequenceNumber, object payload)
        {
            EnqueuePacket(new GameEvent(ClientId, id, sequenceNumber, payload));
        }
        public IBasicEmpyrionApi CreateBasicApi(ILogger logger)
        {
            ThrowIfDisposed();

            return new EpmBasicEmpyrionApi(logger, this);
        }

        public IExtendedEmpyrionApi CreateExtendedApi(ILogger logger)
        {
            ThrowIfDisposed();

            return new EpmExtendedEmpyrionApi(logger, this);
        }

        public void EnqueuePacket(GameEvent gameEvent) 
        {
            ThrowIfDisposed();

            _writerQueue.Enqueue(gameEvent);
            _writerNotifier.Set();
        }

        private async Task ReadSocketAsync(CancellationToken token)
        {
            _logger.LogDebug("Read task started");

            while(!token.IsCancellationRequested)
            {
                await EnsureConnectedAsync(token);

                GameEvent? gameEvent = null;

                try
                {
                    var cmdId = (GameEventId)await _reader.ReadInt32Async(token);
                    var clientId = await _reader.ReadInt32Async(token);
                    var seqNumber = await _reader.ReadUInt16Async(token);

                    var payloadLength = await _reader.ReadInt32Async(token);
                    if (payloadLength != 0)
                    {
                        // TODO: Monitor payload lengths and figure out what a reasonable reusable buffer length would be
                        var buffer = await _reader.ReadAsync(payloadLength, token);
                        var payload = GameEventSerializer.Deserialize(cmdId, buffer);

                        gameEvent = new GameEvent(clientId, (CmdId)cmdId, seqNumber, payload);
                    }
                    else
                    {
                        gameEvent = new GameEvent(clientId, (CmdId)cmdId, seqNumber, null);
                    }
                }
                catch (OperationCanceledException) { } // Ignore, from CTS being canceled during a AsyncBinaryReader read method
                catch (Exception ex)
                {
                    string msg;
                    if (ex is IOException)
                        msg = "Connection closed while reading";
                    else if (ex is ObjectDisposedException)
                        msg = "Connection disposed while reading";
                    else
                        msg = "Error when reading from connection";

                    _logger.LogWarning(ex, msg);
                    await CloseConnectionAsync(token);
                }

                if (gameEvent != null && gameEvent.HasValue)
                    _ = Task.Run(() => { TriggerGameEventHandler(gameEvent.Value); });
            }
        }

        private void TriggerGameEventHandler(GameEvent gameEvent)
        {
            // Eat chat events since EPM doesn't send data
            if ((GameEventId)gameEvent.Id == GameEventId.Event_ChatMessage)
                return;

            // Convert EPM's Event_Chat into a regular Event_ChatMessage
            // This is kind of a hacky work around to get regular mods to work with EPM's chat system
            // Unfortunately the sequence id will probably be messed up, not sure if this will effect anything
            if ((GameEventId)gameEvent.Id == GameEventId.Event_Chat && gameEvent.Payload is MessageData message)
            {
                var payload = ConvertChatMessage(message);
                var newEvent = new GameEvent(gameEvent.ClientId, (CmdId)GameEventId.Event_ChatMessage,
                    gameEvent.SequenceNumber, payload);

                GameEventHandler?.Invoke(newEvent);
            }

            GameEventHandler?.Invoke(gameEvent);
        }

        private static ChatInfo ConvertChatMessage(MessageData message)
        {
            // Weird mapping, sourced from EmpyrionNetApiAccess & server testing
            var type = -1;
            switch (message.Channel)
            {
                case Eleon.MsgChannel.Global:
                    type = 3;
                    break;
                case Eleon.MsgChannel.Faction:
                case Eleon.MsgChannel.Alliance:
                    type = 5;
                    break;
                case Eleon.MsgChannel.SinglePlayer:
                    type = 8;
                    break;
                case Eleon.MsgChannel.Server:
                    type = 9;
                    break;
            }

            return new ChatInfo(message.SenderEntityId, message.Text, 
                message.RecipientEntityId, message.RecipientFaction.Id, (byte)type);
        }

        private async Task WriteSocketAsync(CancellationToken token)
        {
            _logger.LogDebug("Write task started");

            while (!token.IsCancellationRequested)
            {
                await _writerNotifier.WaitAsync(token);

                await EnsureConnectedAsync(token);

                var error = false;
                while (_writerQueue.TryDequeue(out GameEvent gameEvent) && !token.IsCancellationRequested)
                {
                    try
                    {
                        var payload = GameEventSerializer.Serialize((GameEventId)gameEvent.Id, gameEvent.Payload);

                        await _writer.WriteAsync((int)gameEvent.Id, token);
                        await _writer.WriteAsync(gameEvent.ClientId, token);
                        await _writer.WriteAsync(gameEvent.SequenceNumber, token);

                        await _writer.WriteAsync(payload?.Length ?? 0, token);
                        if (payload != null)
                            await _writer.WriteAsync(payload, token);

                        await _writer.FlushAsync(token);
                    }
                    catch (Exception ex)
                    {
                        string msg;
                        if (ex is IOException)
                            msg = "Connection closed while writing";
                        else if (ex is ObjectDisposedException)
                            msg = "Connection disposed while writing";
                        else
                            msg = "Error when writing to connection";

                        _logger.LogWarning(ex, msg);
                        _writerQueue.Enqueue(gameEvent);
                        await CloseConnectionAsync(token);

                        error = true;
                        break;
                    }
                }

                if(!error)
                    _writerNotifier.Reset();
            }
        }

        private async Task EnsureConnectedAsync(CancellationToken cancellationToken)
        {
            var connected = false;
            Task acquiredSemaphoreTask = null;

            try
            {
                acquiredSemaphoreTask = _tcpClientLock.WaitAsync(cancellationToken);
                await acquiredSemaphoreTask;

                while((_tcpClient == null || !_tcpClient.Connected) && 
                    !cancellationToken.IsCancellationRequested)
                {
                    if(_tcpClient == null)
                        _tcpClient = new TcpClient();

                    if(!_tcpClient.Connected)
                    {
                        try
                        {
                            _tcpClient.Connect(_endPoint);

                            var stream = _tcpClient.GetStream();
                            _reader = new AsyncBinaryReader(stream, leaveOpen: true);
                            _writer = new AsyncBinaryWriter(stream, leaveOpen: true);

                            connected = true;
                        }
                        catch (Exception ex)
                        {
                            const int retry = 5 * 1000;
                            if(ex is SocketException _)
                                _logger.LogWarning("TcpClient failed to connect, retrying in {RetrayTime:n0}ms", retry);
                            else
                                _logger.LogWarning(ex, "TcpClient failed to connect, retrying in {RetrayTime:n0}ms", retry);

                            await Task.Delay(retry, cancellationToken);
                        }
                    }
                }
            }
            finally
            {
                if(acquiredSemaphoreTask?.Status == TaskStatus.RanToCompletion)
                    _tcpClientLock.Release();
            }

            if(connected)
            {
                _logger.LogInformation("Connected to {EndPoint}", _endPoint);
                OnConnected?.Invoke();
            }
        }

        private async Task CloseConnectionAsync(CancellationToken cancellationToken)
        {
            Task acquiredSemaphoreTask = null;

            try
            {
                acquiredSemaphoreTask = _tcpClientLock.WaitAsync(cancellationToken);
                await acquiredSemaphoreTask;

                _reader?.Dispose();
                _reader = null;

                _writer?.Dispose();
                _writer = null;

                _tcpClient?.Close();
                _tcpClient = null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to close connection");
                return;
            }
            finally
            {
                if (acquiredSemaphoreTask?.Status == TaskStatus.RanToCompletion)
                    _tcpClientLock.Release();
            }

            OnDisconnected?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfDisposed()
        {
            if (_disposeCount > 0) throw new ObjectDisposedException(GetType().Name);
        }
    }
}
