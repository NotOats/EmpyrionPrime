using Eleon;
using Eleon.Modding;
using EmpyrionPrime.ModFramework.Extensions;
using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;
using EmpyrionPrime.ModFramework;

namespace FrameworkPlugins;

public class NoModInterface : IEmpyrionPlugin
{
    private readonly ILogger _logger;
    private readonly IExtendedEmpyrionApi _empyrionGameApi;
    private readonly IApiRequests _apiRequests;

    public string Name => "NoModInterface";
    public string Author => "NotOats";
    public Version Version => new(1, 0);
    public ModInterface? ModInterface { get; } = null;

    public NoModInterface(
        ILoggerFactory loggerFactory,
        IEmpyrionApiFactory<NoModInterface> apiFactory)
    {
        _logger = loggerFactory.CreateLogger<NoModInterface>("Main");
        _empyrionGameApi = apiFactory.Create<IExtendedEmpyrionApi>();
        _apiRequests = apiFactory.Create<IApiRequests>();

        // ILogger can be used for more indepth logging than ModGameAPI.Console_Write
        _logger.LogInformation("{PluginType} loaded", Name);

        // Console_Write is still available, along with the other ModGameAPI methods
        _empyrionGameApi.ModGameAPI.Console_Write("Using ModGameApi outside of ModInterface!");


        // Access events and make requests via interface
        var apiEvents = apiFactory.Create<IApiEvents>();
        apiEvents.PlayerConnected += async id =>
        {
            var playerInfo = await _apiRequests.PlayerInfo(id);
            var playfieldStats = await _apiRequests.PlayfieldStats(playerInfo.playfield.ToPString());

            _logger.LogInformation("'{PlayerInfo}' loaded into '{PlayfieldName}' (pid {PlayfieldProcessId})",
                playerInfo.playerName, playfieldStats.playfield, playfieldStats.processId);
        };


        // Or even handle chat messages via IExtendedEmpyrionApi
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
