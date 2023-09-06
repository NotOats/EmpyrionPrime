using Eleon.Modding;
using EmpyrionPrime.ModFramework;
using EmpyrionPrime.Plugin;
using EmpyrionPrime.Plugin.Extensions;
using EmpyrionPrime.Plugin.Types;
using Microsoft.Extensions.Logging;

namespace FrameworkPlugins;

public class BasicFrameworkExample : IEmpyrionPlugin
{
    public string Name => "Basic Framework Example";
    public string Author => "NotOats";
    public Version Version => new("1.0");
    public ModInterface? ModInterface => null;

    public BasicFrameworkExample(ILogger logger, IApiEvents apiEvents, IApiRequests apiRequests)
    {
        apiEvents.PlayerConnected += async id =>
        {
            // Welcome the player
            var player = await apiRequests.PlayerInfo(id);
            
            await apiRequests.InGameMessageSinglePlayer(new IdMsgPrio
            {
                id = player.entityId,
                msg = $"Welcome {player.playerName}",
                prio = (byte)MessagePriority.Alert,
                time = 10
            });

            // Log the player and playfield
            var playfield = await apiRequests.PlayfieldStats(player.playfield.ToPString());

            logger.LogInformation("{Player} connected to {Playfield} (pid: {ProcessId}, uptime: {Uptime})",
                player.playerName, playfield.playfield, playfield.processId, playfield.uptime);
        };

        apiEvents.ChatMessage += async chatInfo => 
        {
            var trigger = ".echo-basic ";
            if (!chatInfo.msg.StartsWith(trigger))
                return;

            var message = chatInfo.msg[trigger.Length..];
            if (message == string.Empty)
                return;


            var command = $"say 'Echoing {message}'";
            await apiRequests.ConsoleCommand(command.ToPString());
        };
    }
}
