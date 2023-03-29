using Eleon.Modding;
using EmpyrionPrime.RemoteClient.Epm.Serializers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace EmpyrionPrime.RemoteClient.Epm
{
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
            if(!_disposed)
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
            if(!_started)
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

            while(!token.IsCancellationRequested)
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
                lock(_tcpClientLock)
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
