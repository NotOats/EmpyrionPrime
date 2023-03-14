using Eleon.Modding;
using EmpyrionPrime.Mod.Framework;
using Microsoft.Extensions.Logging;


namespace BaseModPlugin;

internal class ExampleModInterface : BaseMod
{
    public ExampleModInterface(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
        GameEvent += (eventId, seqNr, data) =>
        {
            switch (eventId)
            {
                case CmdId.Event_Player_Connected:
                    if (data is Id payload && payload != null)
                        Task.Factory.StartNew(async () => await HandlePlayerConnected(payload));
                    break;
            }
        };
    }

    private async Task HandlePlayerConnected(Id id)
    {
        Logger.LogInformation("Player Connected - Id: {playerId}", id);

        var playerInfo = (PlayerInfo)await RequestBroker.SendGameRequest(CmdId.Request_Player_Info, id);
        Logger.LogInformation("PlayerInfo - EntityId: {entityId}, Name: {name}, SteamId: {steamId}", 
            playerInfo.entityId, playerInfo.playerName, playerInfo.steamId);
    }
}
