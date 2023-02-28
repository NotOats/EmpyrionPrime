using Eleon.Modding;
using EmpyrionPrime.Mod;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.ExamplePlugin;

public class EntryPoint : IEmpyrionPlugin
{
    public string Name => "Example Plugin";
    public string Author => "NotOats";
    public Version Version => new(1, 0);

    public ModInterface ModInterface { get; }

    // Dependency Injection can be used in IEmpyrionPlugin constructors.
    public EntryPoint(ILogger<EntryPoint> logger)
    {
        logger.LogInformation("Example Plugin loaded");

        // Injected classes can be passed to your ModInterface
        ModInterface = new ExampleModInterface(logger);
    }
}
