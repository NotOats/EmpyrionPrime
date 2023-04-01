using Eleon;
using Eleon.Modding;
using EmpyrionPrime.Plugin.Api;
using EmpyrionPrime.RemoteClient.Epm.Api;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

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

        public EpmClient(ILogger logger, string address = "127.0.0.1", int port = 12345, int clientId = -1)
        {
            _logger = logger;
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
    }
}
