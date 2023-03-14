using Eleon;
using Eleon.Modding;
using EmpyrionPrime.Plugin;
using EmpyrionPrime.RemoteClient;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class RemoteEmpyrionGameApi : IEmpyrionGameApi, IDisposable
{
    private readonly ILogger _logger;
    private readonly IRemoteEmpyrion _remoteEmpyrion;

    public event ChatMessageHandler? ChatMessage;

    public ModGameAPI ModGameAPI { get; }

    public RemoteEmpyrionGameApi(ILogger logger, IRemoteEmpyrion remoteEmpyrion)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _remoteEmpyrion = remoteEmpyrion ?? throw new ArgumentNullException(nameof(remoteEmpyrion));

        ModGameAPI = new RemoteModGameApi(_logger, _remoteEmpyrion);

        _remoteEmpyrion.GameEventHandler += HandleGameEvent;
    }

    public void Dispose()
    {
        _remoteEmpyrion.GameEventHandler -= HandleGameEvent;
    }

    public void SendChatMessage(MessageData messageData)
    {
        // TODO: Handle different types of remote empyrion instances.
        // This is currently hard coded for EPM which listens
        // for eMod_Comands.Request_Chat (200) events
        _remoteEmpyrion.SendRequest((CommandId)200, 0, messageData);
    }

    private void HandleGameEvent(GameEvent gameEvent)
    {
        // Hard coded for EPM which sends eMod_Commands.Event_Chat (201)
        // events on chat message
        if ((int)gameEvent.Id == 201 && gameEvent.Payload is MessageData messageData)
            ChatMessage?.Invoke(messageData);
    }

    private class RemoteModGameApi : ModGameAPI
    {
        private readonly ILogger _logger;
        private readonly IRemoteEmpyrion _remoteEmpyrion;

        public RemoteModGameApi(ILogger logger, IRemoteEmpyrion remoteEmpyrion)
        {
            _logger = logger;
            _remoteEmpyrion = remoteEmpyrion;
        }

        public void Console_Write(string txt)
        {
            _logger.LogInformation("{text}", txt);
        }

        public ulong Game_GetTickTime()
        {
            return (ulong)Environment.TickCount;
        }

        public bool Game_Request(CmdId reqId, ushort seqNr, object data)
        {
            _remoteEmpyrion.SendRequest((CommandId)reqId, seqNr, data);

            return true;
        }
    }
}
