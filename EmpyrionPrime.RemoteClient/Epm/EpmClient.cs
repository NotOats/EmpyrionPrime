using Eleon;
using Eleon.Modding;
using EmpyrionPrime.Plugin.Api;
using EmpyrionPrime.RemoteClient.Epm.Api;
using EmpyrionPrime.RemoteClient.Epm.Serializers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace EmpyrionPrime.RemoteClient.Epm
{
    public class EpmClient : IRemoteEmpyrion, IDisposable
    {
        private readonly ILogger _logger;

        private readonly ThreadedTcpClient _tcpClient;

        private bool _disposed;
        public event Action OnConnected;
        public event Action OnDisconnected;
        public event Action<GameEvent> GameEventHandler;

        public int ClientId { get; }

        public EpmClient(ILogger logger, EpmClientSettings settings)
            : this(logger, settings.IPAddress, settings.Port, settings.ClientId)
        {
            if (!IPAddress.TryParse(settings.IPAddress, out _))
                throw new ArgumentException("Invalid IP address", nameof(settings));

            if (settings.Port < 1 || settings.Port > 65535)
                throw new ArgumentException("Invalid port", nameof(settings));

            if(logger == null)
                throw new ArgumentNullException(nameof(logger));
        }

        public EpmClient(ILogger logger, string address = "127.0.0.1", int port = 12345, int clientId = -1)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ClientId = clientId == -1 ? Process.GetCurrentProcess().Id : clientId;

            _tcpClient = new ThreadedTcpClient(_logger, address, port);
            _tcpClient.OnConnected += () => { OnConnected?.Invoke(); };
            _tcpClient.OnDisconnected += () => { OnDisconnected?.Invoke(); };
            _tcpClient.GameEventHandler += (gameEvent) =>
            {
                GameEventHandler?.Invoke(gameEvent);

                // Convert EPM's Event_Chat into a regular Event_ChatMessage
                // This is kind of a hacky work around to get regular mods to work with EPM's chat system
                // Unfortunately the sequence id will probably be messed up, not sure if this will effect anything
                if((GameEventId)gameEvent.Id == GameEventId.Event_Chat && gameEvent.Payload is MessageData message)
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

                    var chatInfo = new ChatInfo(message.SenderEntityId, message.Text, message.RecipientEntityId, message.RecipientFaction.Id, (byte)type);
                    var chatEvent = new GameEvent(gameEvent.ClientId, (CmdId)GameEventId.Event_ChatMessage, gameEvent.SequenceNumber, chatInfo);

                    GameEventHandler?.Invoke(chatEvent);
                }
            };
        }

        public void Dispose()
        {
            if(!_disposed)
            {
                _tcpClient.Dispose();

                _disposed = true;
            }
        }

        public void Start()
        {
            _tcpClient.Start();
        }

        public void SendRequest(CmdId id, ushort sequenceNumber, object payload)
        {
            var request = new GameEvent(ClientId, id, sequenceNumber, payload);

            _tcpClient.SendPacket(request);
        }

        public IBasicEmpyrionApi CreateBasicApi(ILogger logger)
        {
            return new EpmBasicEmpyrionApi(logger, this);
        }

        public IExtendedEmpyrionApi CreateExtendedApi(ILogger logger)
        {
            return new EpmExtendedEmpyrionApi(logger, this);
        }

        internal class ThreadedTcpClient : IDisposable
        {
            private readonly ILogger _logger;
            private readonly string _ipAddress;
            private readonly int _port;

            private readonly object _tcpClientLock = new object();
            private TcpClient _tcpClient = null;
            private BinaryReader _tcpReader = null;
            private BinaryWriter _tcpWriter = null;

            private readonly CancellationTokenSource _threadCts = new CancellationTokenSource();
            private readonly Thread _readThread;
            private readonly Thread _writeThread;

            private readonly ManualResetEventSlim _writeNotifier = new ManualResetEventSlim(false);
            private readonly ConcurrentQueue<GameEvent> _writeQueue = new ConcurrentQueue<GameEvent>();

            private bool _disposed = false;
            private volatile bool _started = false;

            public event Action OnConnected;
            public event Action OnDisconnected;
            public event Action<GameEvent> GameEventHandler;

            public ThreadedTcpClient(ILogger logger, string ipAddress, int port)
            {
                _logger = logger;
                _ipAddress = ipAddress;
                _port = port;

                _readThread = new Thread(ReadThread)
                {
                    IsBackground = true,
                    Name = "EmpClient-ReadThread"
                };

                _writeThread = new Thread(WriteThread)
                {
                    IsBackground = true,
                    Name = "EmpClient-WriteThread"
                };
            }

            public void Dispose()
            {
                if (!_disposed)
                {
                    _threadCts.Cancel();

                    if (!_readThread.Join(1000))
                    {
                        _logger.LogError("Failed to stop read thread, interrupting...");
                        _readThread.Interrupt();
                    }

                    if (!_writeThread.Join(1000))
                    {
                        _logger.LogError("Failed to stop write thread, interrupting...");
                        _writeThread.Interrupt();
                    }

                    _tcpClient?.Close();
                    _tcpClient = null;

                    _disposed = true;
                }
            }

            public void Start()
            {
                if (!_started)
                {
                    _readThread.Start(_threadCts.Token);
                    _writeThread.Start(_threadCts.Token);
                    _started = true;
                }
            }

            public void SendPacket(GameEvent gameEvent)
            {
                _writeQueue.Enqueue(gameEvent);
                _writeNotifier.Set();
            }

            private void ReadThread(object cancellationToken)
            {
                _logger.LogDebug("Read thread started (id {ThreadId})", Thread.CurrentThread.ManagedThreadId);

                var token = (CancellationToken)cancellationToken;

                while (!token.IsCancellationRequested)
                {
                    EnsureConnected();

                    GameEvent? gameEvent = null;

                    try
                    {
                        var commandId = (GameEventId)_tcpReader.ReadInt32();
                        var clientId = _tcpReader.ReadInt32();
                        var sequenceNumber = _tcpReader.ReadUInt16();

                        var payloadLength = _tcpReader.ReadInt32();
                        if (payloadLength != 0)
                        {
                            var buffer = new byte[payloadLength];
                            var read = 0;

                            do
                            {
                                read += _tcpReader.Read(buffer, read, payloadLength - read);
                            } while (read < payloadLength && !token.IsCancellationRequested);

                            var payload = GameEventSerializer.Deserialize(commandId, buffer);
                            gameEvent = new GameEvent(clientId, (CmdId)commandId, sequenceNumber, payload);
                        }
                        else
                        {
                            gameEvent = new GameEvent(clientId, (CmdId)commandId, sequenceNumber, null);
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
                        GameEventHandler?.Invoke(gameEvent.Value);
                }
            }

            private void WriteThread(object cancellationToken)
            {
                _logger.LogDebug("Write thread started (id {ThreadId})", Thread.CurrentThread.ManagedThreadId);

                var token = (CancellationToken)cancellationToken;

                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        _writeNotifier.Wait(token);
                    }
                    catch (OperationCanceledException) { }

                    EnsureConnected();

                    try
                    {
                        while (_writeQueue.TryDequeue(out GameEvent gameEvent) && !token.IsCancellationRequested)
                        {
                            var payload = GameEventSerializer.Serialize((GameEventId)gameEvent.Id, gameEvent.Payload);

                            _tcpWriter.Write((int)gameEvent.Id);
                            _tcpWriter.Write(gameEvent.ClientId);
                            _tcpWriter.Write(gameEvent.SequenceNumber);

                            _tcpWriter.Write(payload?.Length ?? 0);
                            if (payload != null)
                                _tcpWriter.Write(payload);

                            _tcpWriter.Flush();
                        }
                    }
                    catch (IOException ex)
                    {
                        _logger.LogWarning(ex, "Connection closed while writing");
                        CloseConnection();
                    }
                    catch (ObjectDisposedException ex)
                    {
                        _logger.LogWarning(ex, "Connection disposed while writing");
                        CloseConnection();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Error when writing to connection");
                        CloseConnection();
                    }

                    _writeNotifier.Reset();
                }
            }

            private void EnsureConnected()
            {
                lock (_tcpClientLock)
                {
                    if (_tcpClient != null && _tcpClient.Connected)
                        return;

                    _logger.LogDebug("Connecting to {server_address}:{server_port}", _ipAddress, _port);

                    _tcpClient = new TcpClient(_ipAddress, _port);

                    var stream = _tcpClient.GetStream();
                    _tcpReader = new BinaryReader(stream);
                    _tcpWriter = new BinaryWriter(stream);

                    _logger.LogInformation("Connected to {server_address}:{server_port}", _ipAddress, _port);

                    OnConnected?.Invoke();
                }
            }

            private void CloseConnection()
            {
                try
                {
                    lock (_tcpClientLock)
                    {
                        if (_tcpClient == null)
                            return;

                        _tcpClient.Close();
                        _tcpClient = null;
                    }

                    OnDisconnected?.Invoke();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to close connection");
                }
            }
        }
    }
}
