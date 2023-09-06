using Eleon;
using Eleon.Modding;
using EmpyrionPrime.Plugin;
using EmpyrionPrime.Plugin.Api;
using EmpyrionPrime.Plugin.Extensions;
using Microsoft.Extensions.Logging;
using EmpyrionPrime.ModFramework;
using EmpyrionPrime.ModFramework.Configuration;

namespace FrameworkPlugins;

public class PluginOptions
{
    public string PluginName { get; set; } = "NoModInterface";
    public string Author { get; set; } = "NotOats";
    public string Version { get; set; } = "1.0";
}

public class NoModInterface : IEmpyrionPlugin
{
    private readonly ILogger _logger;
    private readonly IExtendedEmpyrionApi _empyrionGameApi;
    private readonly IApiRequests _apiRequests;
    private readonly IPluginOptions<PluginOptions> _options;

    public string Name => _options.Value.PluginName;
    public string Author => _options.Value.Author;
    public Version Version => new(_options.Value.Version);
    public ModInterface? ModInterface { get; } = null;

    public NoModInterface(
        ILogger logger,
        IEmpyrionApiFactory apiFactory,
        IApiEvents apiEvents,
        IPluginOptionsFactory optionsFactory,
        ModGameAPI modGameApi)
    {
        _logger = logger;
        _empyrionGameApi = apiFactory.Create<IExtendedEmpyrionApi>();
        _apiRequests = apiFactory.Create<IApiRequests>();

        // Robust options system built in that uses appsettings.json, appsettings.<Environment>.json, and PLUGINNAME_ prefixed environment variables
        _options = optionsFactory.Get<PluginOptions>();
        _options.OptionsChanged += () => _logger.LogInformation("Options changed!");

        // ILogger can be used for more in depth logging than ModGameAPI.Console_Write
        _logger.LogInformation("{PluginType} loaded", Name);

        // Console_Write is still available, along with the other ModGameAPI methods
        modGameApi.Console_Write("Using ModGameApi outside of ModInterface!");


        // Access events and make requests via interface
        apiEvents.PlayerConnected += async id =>
        {
            var playerInfo = await _apiRequests.PlayerInfo(id);
            var playfieldStats = await _apiRequests.PlayfieldStats(playerInfo.playfield.ToPString());

            _logger.LogInformation("'{PlayerInfo}' loaded into '{PlayfieldName}' (pid {PlayfieldProcessId})",
                playerInfo.playerName, playfieldStats.playfield, playfieldStats.processId);

            await _apiRequests.InGameMessageSinglePlayer(new IdMsgPrio
            {
                id = playerInfo.entityId,
                msg = $"Welcome {playerInfo.playerName}!",
                prio = 1,
                time = 20,
            });
        };


        // Or even handle chat messages via IExtendedEmpyrionApi
        _empyrionGameApi.ChatMessage += messageData =>
        {
            var trigger = ".echo-no ";
            if (!messageData.Text.StartsWith(trigger))
                return;

            var message = messageData.Text[trigger.Length..];
            if (message == string.Empty)
                return;

            _logger.LogInformation("Echoing {EntityId}: {Text}", messageData.SenderEntityId, message);

            var response = new MessageData
            {
                SenderType = Eleon.SenderType.System,
                Channel = Eleon.MsgChannel.Global,
                SenderNameOverride = "Echo",
                Text = message
            };

            _empyrionGameApi.SendChatMessage(response);

            // How to run async code inside ChatMessage events
            _ = Task.Run(async () =>
            {
                await _apiRequests.InGameMessageSinglePlayer(new IdMsgPrio
                {
                    id = messageData.SenderEntityId,
                    msg = message,
                    prio = 1,
                    time = 5,
                });
            });
        };
    }
}
