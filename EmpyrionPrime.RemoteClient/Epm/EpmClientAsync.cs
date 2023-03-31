using Eleon.Modding;
using EmpyrionPrime.Plugin;
using EmpyrionPrime.RemoteClient.Epm.Api;
using EmpyrionPrime.RemoteClient.Epm.Serializers;
using EmpyrionPrime.RemoteClient.Streams;
using Microsoft.Extensions.Logging;
using Nito.AsyncEx;
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

        private readonly object _tcpClientLock = new object();
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
                throw new ArgumentException("Invalid ip address", nameof(ipString));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _endPoint = new IPEndPoint(address, port);

            ClientId = clientId == -1 ? Process.GetCurrentProcess().Id : clientId;
        }

        public void Dispose()
        {
            if (Interlocked.Increment(ref _disposeCount) != 1)
                return;

            _shutdownCts.Cancel();

            // Save Accept & Listen tasks, task.Wait() until they're finished. Maybe use IAsyncDisposable?
            var finished = Task.WaitAll(new[] { _readerTask, _writerTask }, 10 * 1000);
            if(!finished)
            {
                _logger.LogError("Reader/Writer tasks failed to stop in time.");
            }

            CloseConnection();
        }

        public void Start()
        {
            lock(_taskLock)
            {
                // TODO: Figure out if Task.Run should also get a CancellationToken
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
            return new EpmBasicEmpyrionApi(logger, this);
        }

        public IExtendedEmpyrionApi CreateExtendedApi(ILogger logger)
        {
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
                EnsureConnected();

                GameEvent? gameEvent = null;

                try
                {
                    var cmdId = (GameEventId)await _reader.ReadInt32Async(token);
                    var clientId = await _reader.ReadInt32Async(token);
                    var seqNumber = await _reader.ReadUInt16Async(token);

                    var payloadLength = await _reader.ReadInt32Async(token);
                    if(payloadLength != 0)
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
                catch (IOException ex)
                {
                    _logger.LogWarning(ex, "Connection closed while reading");
                    CloseConnection();
                }
                catch (ObjectDisposedException ex)
                {
                    _logger.LogWarning(ex, "Connection disposed while reading");
                    CloseConnection();
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error when reading to connection");
                    CloseConnection();
                }

                if (gameEvent != null && gameEvent.HasValue)
                    _ = Task.Run(() => { GameEventHandler?.Invoke(gameEvent.Value); });
            }
        }

        private async Task WriteSocketAsync(CancellationToken token)
        {
            _logger.LogDebug("Write task started");

            while (!token.IsCancellationRequested)
            {
                await _writerNotifier.WaitAsync(token);

                EnsureConnected();

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

                        await _writer.FlushAsync();
                    }
                    catch(IOException ex)
                    {
                        _logger.LogWarning(ex, "Connection closed while writing");
                        error = true;
                    }
                    catch (ObjectDisposedException ex)
                    {
                        _logger.LogWarning(ex, "Connection disposed while writing");
                        error = true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Error when writing to connection");
                        error = true;
                    }

                    if(error)
                    {
                        _writerQueue.Enqueue(gameEvent);
                        CloseConnection();
                        break;
                    }
                }

                if(!error)
                    _writerNotifier.Reset();
            }
        }

        private void EnsureConnected()
        {
            var connected = false;

            lock (_tcpClientLock)
            {
                if (_tcpClient != null && _tcpClient.Connected)
                    return;

                _logger.LogDebug("Connecting to {EndPoint}", _endPoint);

                _tcpClient = new TcpClient();
                _tcpClient.Connect(_endPoint);

                var stream = _tcpClient.GetStream();
                _reader = new AsyncBinaryReader(stream, leaveOpen: true);
                _writer = new AsyncBinaryWriter(stream, leaveOpen: true);

                connected = true;
            }

            if(connected)
            {
                _logger.LogInformation("Connected to {EndPoint}", _endPoint);
                OnConnected?.Invoke();
            }
        }

        private void CloseConnection()
        {
            try
            {
                lock(_tcpClientLock)
                {
                    _reader?.Dispose();
                    _reader = null;

                    _writer?.Dispose();
                    _writer = null;

                    _tcpClient?.Close();
                    _tcpClient = null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to close connection");
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
