using Eleon;
using EmpyrionPrime.Plugin;
using EmpyrionPrime.RemoteClient;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class RemoteExtendedEmpyrionApi<TPlugin> : RemoteBasicEmpyrionApi<TPlugin>, IExtendedEmpyrionApi where TPlugin : IEmpyrionPlugin
{
    public event ChatMessageHandler? ChatMessage;

    public RemoteExtendedEmpyrionApi(ILoggerFactory loggerFactory, IRemoteEmpyrion remoteEmpyrion) 
        : base(loggerFactory, remoteEmpyrion)
    {
        if(loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));
        if (remoteEmpyrion == null) throw new ArgumentNullException(nameof(remoteEmpyrion));
    }

    public void SendChatMessage(MessageData messageData)
    {
        // TODO: Handle different types of remote empyrion instances.
        // This is currently hard coded for EPM which listens
        // for eMod_Comands.Request_Chat (200) events
        RemoteEmpyrion.SendRequest((CommandId)200, 0, messageData);
    }

    protected override void HandleGameEvent(GameEvent gameEvent)
    {
        base.HandleGameEvent(gameEvent);

        // Hard coded for EPM which sends eMod_Commands.Event_Chat (201)
        // events on chat message
        if ((int)gameEvent.Id == 201 && gameEvent.Payload is MessageData messageData)
            ChatMessage?.Invoke(messageData);
    }
}
