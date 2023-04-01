# EmpyrionPrime.Plugin
Basic interfaces for developing [EmpyrionPrime](https://github.com/NotOats/EmpyrionPrime) plugins.

## Examples
You can find some example plugins in at the [Example Plugin](https://github.com/NotOats/EmpyrionPrime/tree/master/ExamplePlugins) repository.

### Basic Plugin
Below is an extremely simple plugin example.

```csharp
public class BasicPlugin : IEmpyrionPlugin
{
    public string Name => "Basic Plugin";
    public string Author => "Author";
    public Version Version => new(1, 0);
    public ModInterface ModInterface { get; }

    public BasicPlugin(IBasicEmpyrionApi basicEmpyrionApi)
    {
        // Set the ModInterface
        ModInterface = ExampleModInterface();

        // Or use Empyrion api interfaces via dependency injection 
        basicEmpyrionApi.GameEvent += (eventId, seqNum, data) =>
        {
            // Do something
        };
    }
}
```