using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace EmpyrionPrime.RemoteClient.Epm
{
    public class EpmClient : IRemoteEmpyrion, IDisposable
    {
        private readonly ILogger<EpmClient> _logger;

        private readonly ThreadedTcpClient _tcpClient;

        private bool _disposed;
        public event Action OnConnected;
        public event Action OnDisconnected;
        public event Action<GameEvent> GameEventHandler;

        public int ClientId { get; }

        public EpmClient(ILogger<EpmClient> logger, string address = "127.0.0.1", int port = 12345, int clientId = -1)
        {
            _logger = logger;
            ClientId = clientId == -1 ? Process.GetCurrentProcess().Id : clientId;

            _tcpClient = new ThreadedTcpClient(_logger, address, port);
            _tcpClient.OnConnected += () => { OnConnected?.Invoke(); };
            _tcpClient.OnDisconnected += () => { OnDisconnected?.Invoke(); };
            _tcpClient.GameEventHandler += (gameEvent) => { GameEventHandler?.Invoke(gameEvent); };
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

        public void SendRequest(CommandId id, ushort sequenceNumber, object payload)
        {
            var request = new GameEvent(ClientId, id, sequenceNumber, payload);

            _tcpClient.SendPacket(request);
        }
    }
}
