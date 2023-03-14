using Eleon.Modding;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace BasicPlugin;

public class EntryPoint : IEmpyrionPlugin
{
    public string Name => "Basic Plugin";
    public string Author => "NotOats";
    public Version Version => new(1, 0);

    public ModInterface ModInterface { get; }

    // Dependency Injection can be used in IEmpyrionPlugin constructors.
    public EntryPoint(ILogger<EntryPoint> logger)
    {
        logger.LogInformation("Basic Plugin loaded");

        // Injected classes can be passed to your ModInterface
        ModInterface = new ExampleModInterface(logger);
    }
}
