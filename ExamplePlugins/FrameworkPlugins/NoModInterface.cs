using Eleon;
using Eleon.Modding;
using EmpyrionPrime.ModFramework.Extensions;
using EmpyrionPrime.Plugin;
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
        ILoggerFactory loggerFactory,
        IEmpyrionApiFactory<NoModInterface> apiFactory,
        IPluginOptionsFactory<NoModInterface> optionsFactory)
    {
        _logger = loggerFactory.CreateLogger<NoModInterface>("Main");
        _empyrionGameApi = apiFactory.Create<IExtendedEmpyrionApi>();
        _apiRequests = apiFactory.Create<IApiRequests>();

        // Robust options system built in that uses appsettings.json, appsettings.<Environment>.json, and PLUGINNAME_ prefixed environment variables
        _options = optionsFactory.Get<PluginOptions>();
        _options.OptionsChanged += () => _logger.LogInformation("Options changed!");

        // ILogger can be used for more indepth logging than ModGameAPI.Console_Write
        _logger.LogInformation("{PluginType} loaded", Name);

        // Console_Write is still available, along with the other ModGameAPI methods
        // TODO: ModGameApi DI injection via SimpleInjector's RegisterConditional
        //_empyrionGameApi.ModGameAPI.Console_Write("Using ModGameApi outside of ModInterface!");


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
