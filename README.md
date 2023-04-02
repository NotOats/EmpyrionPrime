# EmpyrionPrime
**Empyrion Piggybacked Remote Independent Modification Environment** is a utility to host Empyrion mods remotely using EPM which is bundled with the game server.

## Dependency Injection Support
Plugins can access the following interfaces in their constructor.

| Feature                | Plugin                                                              | ModFramework                                                                                 |
|------------------------|---------------------------------------------------------------------|----------------------------------------------------------------------------------------------|
| Event Handling         | IBasicEmpyrionApi.GameEvent<br>IExtendedEmpyrionApi.ChatMessage     | IApiEvents                                                                                   |
| Event Requests         | IBasicEmpyrionApi.SendEvent<br>IExtendedEmpyrionApi.SendChatMessage | IApiRequests<br>IRequestBroker                                                               |
| Empyrion Configuration | N/A                                                                 | IEmpyrionEnvironment<br>IEmpyrionServerConfig<br>IEmpyrionGameConfig<br>IEmpyrionAdminConfig |
| Plugin Configuration   | N/A                                                                 | IPluginOptionsFactory\<TSettings>                                                            |

Note: ModFramework can use everything in the Plugin section as well.

## Projects

### Launcher
Used to host mod outside of the game server. By default it will connect to the game server via EPM (the mod EAH uses to connect to the server).
\
\
Plugins should be assemblies with a class that implements
 - IEmpyrionPlugin ([EmpyrionPrime.Plugin](https://www.nuget.org/packages/EmpyrionPrime.Plugin)): preferred method due to more flexibility in plugin creation. 
   This also supports dependency injection in the plugin's constructor.
   \
   Plugins can also make use of [EmpyrionPrime.ModFramework](https://www.nuget.org/packages/EmpyrionPrime.ModFramework) for more APIs and utilities.

 - ModInterface (Eleon.Modding): legacy Empyrion mod framework from Eleon, this is supported but not guaranteed to work with existing mods.

### Plugin
Simple plugin interface as well as some Eleon mod API extensions and types.

#### Basic Plugin Example
For more examples see the `ExamplePlugins` folder

 ```csharp
public class BasicPlugin : IEmpyrionPlugin
{
    public string Name => "Basic Plugin";
    public string Author => "NotOats";
    public Version Version => new(1, 0);

    public ModInterface ModInterface { get; }

    public BasicPlugin(ILogger logger, IBasicEmpyrionApi basicEmpyrionApi)
    {
        // Use a Eleon legacy style ModInterface
        ModInterface = new ExampleModInterface();

        // Or use the Plugin API interfaces
        basicEmpyrionApi.GameEvent += (eventId, seqNum, data) =>
        {
            logger.LogDebug("GameEvent(id: {EventId}, Seq: {SequenceNumber}, Data: {DataType})",
                eventId, seqNum, data?.GetType());

            // Do something
        };
    }
}
 ```

 
### ModFramework
Framework for working with Plugins.

#### Framework Plugin Example
For more examples see the `FrameworkPlugins` project in ExamplePlugins.

```csharp
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
    }
}
```