using Eleon;
using Eleon.Modding;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;
using System;

namespace EmpyrionPrime.RemoteClient.Epm.Api
{
    internal class EpmExtendedEmpyrionApi : EpmBasicEmpyrionApi, IExtendedEmpyrionApi
    {
        public event ChatMessageHandler ChatMessage;

        public EpmExtendedEmpyrionApi(ILogger logger, IRemoteEmpyrion client)
            : base(logger, client)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (client == null) throw new ArgumentNullException(nameof(client));
        }

        public void SendChatMessage(MessageData messageData)
        {
            Client.SendRequest((CmdId)GameEventId.Event_Chat, 0, messageData);
        }

        protected override void HandleGameEvent(GameEvent gameEvent)
        {
            base.HandleGameEvent(gameEvent);

            if ((GameEventId)gameEvent.Id == GameEventId.Event_Chat 
                && gameEvent.Payload is MessageData data)
            {
                ChatMessage?.Invoke(data);
            }
        }
    }
}
