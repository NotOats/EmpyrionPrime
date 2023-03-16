using Eleon;
using Eleon.Modding;
using EmpyrionPrime.ModFramework.Extensions;
using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace FrameworkPlugins;

public class NoModInterface : IEmpyrionPlugin
{
    private readonly ILogger _logger;
    private readonly IEmpyrionGameApi<NoModInterface> _empyrionGameApi;

    public string Name => "NoModInterface";
    public string Author => "NotOats";
    public Version Version => new(1, 0);
    public ModInterface? ModInterface { get; } = null;

    public NoModInterface(
        //ILogger<NoModInterface> logger, 
        ILoggerFactory loggerFactory,
        IEmpyrionGameApi<NoModInterface> empyrionGameApi,
        IApiEvents<NoModInterface> apiEvents,
        IApiRequests<NoModInterface> apiRequests)
    {
        //_logger = logger;
        _logger = loggerFactory.CreateLogger<NoModInterface>("Main");
        _empyrionGameApi = empyrionGameApi;

        // ILogger can be used for more indepth logging than ModGameAPI.Console_Write
        _logger.LogInformation("{PluginType} loaded", Name);

        // Console_Write is still available, along with the other ModGameAPI methods
        _empyrionGameApi.ModGameAPI.Console_Write("Using ModGameApi outside of ModInterface!");


        // Access ApiEvents and ApiRequests directly
        apiEvents.PlayerConnected += async id =>
        {
            var playerInfo = await apiRequests.PlayerInfo(id);
            var playfieldStats = await apiRequests.PlayfieldStats(playerInfo.playfield.ToPString());

            _logger.LogInformation("'{PlayerInfo}' loaded into '{PlayfieldName}' (pid {PlayfieldProcessId})",
                playerInfo.playerName, playfieldStats.playfield, playfieldStats.processId);
        };


        // Or even handle chat messages via IEmpyrionGameApi
        _empyrionGameApi.ChatMessage += messageData =>
        {
            _logger.LogInformation("Echoing {EntityId}: {Text}", messageData.SenderEntityId, messageData.Text);

            var response = new MessageData
            {
                SenderType = Eleon.SenderType.System,
                Channel = Eleon.MsgChannel.Global,
                SenderNameOverride = "Echo",
                Text = messageData.Text
            };

            _empyrionGameApi.SendChatMessage(response);
        };
    }
}
