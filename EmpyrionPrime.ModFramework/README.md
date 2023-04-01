# EmpyrionPrime.ModFramework
Extended interfaces and apis for developing [EmpyrionPrime](https://github.com/NotOats/EmpyrionPrime) plugins.

## Examples
You can find some example plugins in at the [Example Plugin](https://github.com/NotOats/EmpyrionPrime/tree/master/ExamplePlugins) repository.

### Basic Plugin
Below is an extremely simple plugin example.

```csharp
public class BasicPlugin : IEmpyrionPlugin
{
    private readonly ILogger _logger;
    private readonly IApiRequests _apiRequests;

    public string Name => "Basic Plugin";
    public string Author => "Author";
    public Version Version => new(1, 0);
    public ModInterface ModInterface { get; } = null;

    public BasicPlugin(ILogger logger, IApiRequests apiRequests, IApiEvents apiEvents)
    {
        _logger = logger;
        _apiRequests = apiRequests;

        // Access events and make requests via interface
        apiEvents.PlayerConnected += async id =>
        {
            var playerInfo = await _apiRequests.PlayerInfo(id);
            var playfieldStats = await _apiRequests.PlayfieldStats(playerInfo.playfield.ToPString());

            _logger.LogInformation("'{PlayerInfo}' loaded into '{PlayfieldName}' (pid {PlayfieldProcessId})",
                playerInfo.playerName, playfieldStats.playfield, playfieldStats.processId);
        };
    }
}
```