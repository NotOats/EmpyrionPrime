using Eleon.Modding;
using EmpyrionPrime.Mod;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.ExamplePlugin;

public class EntryPoint : IEmpyrionPlugin
{
    public string Name { get; } = "Example Plugin";

    public Version Version { get; } = new Version(1, 0);

    public ModInterface ModInterface { get; }

    public EntryPoint(ILogger<EntryPoint> logger)
    {
        // Dependency Injection is used in IEmpyrionPlugin constructors.
        logger.LogInformation("Example Plugin loaded");

        // Injected classes can be passed to your ModInterface
        ModInterface = new ExampleModInterface(logger);
    }
}
