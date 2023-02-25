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
            _logger.LogInformation($"Read thread started (id {Thread.CurrentThread.ManagedThreadId})");

            var token = (CancellationToken)cancellationToken;

            while(!token.IsCancellationRequested)
            {
                EnsureConnected();

                var gameEvent = ProcessRead(token);
                
                if(gameEvent != null && gameEvent.HasValue)
                    GameEventHandler?.Invoke(gameEvent.Value);
            }
        }

        private void WriteThread(object cancellationToken)
        {
            _logger.LogInformation($"Write thread started (id {Thread.CurrentThread.ManagedThreadId})");

            var token = (CancellationToken)cancellationToken;

            while (!token.IsCancellationRequested)
            {
                try
                {
                    _writeNotifier.Wait(token);
                }
                catch (OperationCanceledException) { }

                EnsureConnected();
                ProcessWriteQueue(token);

                _writeNotifier.Reset();
            }
        }

        private void EnsureConnected()
        {
            lock (_tcpClientLock)
            {
                if (_tcpClient != null && _tcpClient.Connected)
                    return;

                _logger.LogInformation($"Connecting to {_ipAddress}:{_port}");

                _tcpClient = new TcpClient(_ipAddress, _port);

                var stream = _tcpClient.GetStream();
                _tcpReader = new BinaryReader(stream);
                _tcpWriter = new BinaryWriter(stream);

                _logger.LogInformation("Connected and created stream reader/writer");

                OnConnected?.Invoke();
            }
        }

        private GameEvent? ProcessRead(CancellationToken cancellationToken)
        {
            try
            {
                var commandId = (CommandId)_tcpReader.ReadInt32();
                var clientId = _tcpReader.ReadInt32();
                var sequenceNumber = _tcpReader.ReadUInt16();

                var payloadLength = _tcpReader.ReadInt32();
                if (payloadLength == 0)
                    return new GameEvent(clientId, commandId, sequenceNumber, null);

                var buffer = new byte[payloadLength];
                var read = 0;

                do
                {
                    read += _tcpReader.Read(buffer, read, payloadLength - read);
                } while (read < payloadLength && !cancellationToken.IsCancellationRequested);

                var payload = CommandSerializer.Deserialize(commandId, buffer);
                return new GameEvent(clientId, commandId, sequenceNumber, payload);
            }
            catch (IOException ex)
            {
                _logger.LogWarning(ex, "Connection closed while reading");
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

            return null;
        }

        private void ProcessWriteQueue(CancellationToken cancellationToken)
        {
            try
            {
                while(_writeQueue.TryDequeue(out GameEvent gameEvent) && !cancellationToken.IsCancellationRequested)
                {
                    var payload = CommandSerializer.Serialize(gameEvent.Id, gameEvent.Payload);

                    _tcpWriter.Write((int)gameEvent.Id);
                    _tcpWriter.Write(gameEvent.ClientId);
                    _tcpWriter.Write(gameEvent.SequenceNumber);

                    _tcpWriter.Write(payload?.Length ?? 0);
                    if(payload != null)
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
