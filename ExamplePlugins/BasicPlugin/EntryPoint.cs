using Eleon;
using Eleon.Modding;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace BasicPlugin;

public class EntryPoint : IEmpyrionPlugin
{
    public string Name => "Basic Plugin";
    public string Author => "NotOats";
    public Version Version => new(1, 0);

    public ModInterface? ModInterface { get; }

    // Dependency Injection can be used in IEmpyrionPlugin constructors.
    public EntryPoint(ILogger<EntryPoint> logger, IEmpyrionGameApiFactory gameApiFactory)
    {
        // ILogger can be used for more indepth logging than ModGameAPI.Console_Write
        logger.LogInformation("Basic Plugin loaded");

        // Access IEmpyrionGameApi for current/future custom methods
        var gameApi = gameApiFactory.CreateGameApi<EntryPoint>();

        // This can also be used to access ModGameAPI before the ModInterface is loaded
        gameApi.ModGameAPI.Console_Write("Loaded ModGameApi before ModInterface");

        // Or even handle chat messages
        gameApi.ChatMessage += messageData =>
        {
            var response = new MessageData
            {
                SenderType = Eleon.SenderType.System,
                Channel = Eleon.MsgChannel.Global,
                SenderNameOverride = "ExampleMod",
                Text = messageData.Text
            };

            gameApi.SendChatMessage(response);
        };

        // For your standard existing ModInterface plugins
        ModInterface = new ExampleModInterface();

        // This can even be null if you don't want to implement ModInterface
        //ModInterface = null;
    }
}
