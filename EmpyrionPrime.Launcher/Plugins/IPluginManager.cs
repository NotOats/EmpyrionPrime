using EmpyrionPrime.Plugin;

namespace EmpyrionPrime.Launcher.Plugins;

internal interface IPluginManager : IDisposable
{
    IEnumerable<IPluginHost> Hosts { get; }

    void ExecuteOnEachHost(Action<IPluginHost> action);
    void ExecuteOnEachPlugin(Action<IEmpyrionPlugin> action);
}
