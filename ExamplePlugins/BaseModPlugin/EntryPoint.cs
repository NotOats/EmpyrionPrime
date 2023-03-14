using Eleon.Modding;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace BaseModPlugin;

public class EntryPoint : IEmpyrionPlugin
{
    public string Name => "BaseMod Plugin";
    public string Author => "NotOats";
    public Version Version => new(1, 0);

    public ModInterface ModInterface { get; }

    public EntryPoint(ILoggerFactory loggerFactory)
    {
        ModInterface = new ExampleModInterface(loggerFactory);
    }
}
