using Eleon.Modding;
using EmpyrionPrime.ModFramework;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace FrameworkPlugins;

public class BasicModInterface : IEmpyrionPlugin
{
    public string Name => "BasicModInterface";
    public string Author => "NotOats";
    public Version Version => new(1, 0);
    public ModInterface ModInterface { get; }

    public BasicModInterface(ILoggerFactory loggerFactory)
    {
        ModInterface = new ExampleModInterface(loggerFactory);
    }

    private class ExampleModInterface : ModInterfaceBase
    {
        public ExampleModInterface(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            GameStarting += () =>
            {
                ApiEvents.PlayerConnected += HandlePlayerConnected;
            };
        }

        private async Task HandlePlayerConnected(Id id)
        {
            Logger.LogInformation("Player Connected - Id: {playerId}", id);

            var playerInfo = await ApiRequests.PlayerInfo(id);
            Logger.LogInformation("PlayerInfo - EntityId: {entityId}, Name: {name}, SteamId: {steamId}",
                playerInfo.entityId, playerInfo.playerName, playerInfo.steamId);
        }
    }

}
