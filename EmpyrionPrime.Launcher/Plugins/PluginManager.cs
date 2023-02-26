using EmpyrionPrime.Launcher.Collections;
using EmpyrionPrime.Mod;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmpyrionPrime.Launcher.Plugins;

internal class PluginManager : IPluginManager
{
    private readonly ILogger<PluginManager> _logger;
    private readonly IOptions<PluginsSettings> _settings;
    private readonly IServiceProvider _serviceProvider;

    private readonly object _hostsLock = new();
    private readonly List<PluginHost> _hosts = new();

    public IEnumerable<IPluginHost> Hosts
    {
        get { return _hosts.AsLocked(_hostsLock); }
    }

    public string PluginFolder => Path.Join(Environment.CurrentDirectory, _settings.Value.Folder);

    public PluginManager(ILogger<PluginManager> logger, IOptions<PluginsSettings> settings, IServiceProvider provider) 
    {
        _logger = logger;
        _settings = settings;
        _serviceProvider = provider;

        // Load plugins
        var pluginPath = Path.Join(Environment.CurrentDirectory, _settings.Value.Folder);
        if (!Directory.Exists(pluginPath))
        {
            _logger.LogWarning("Plugin Path '{PluginPath}' does not exist", pluginPath);
            return;
        }

        foreach (var path in PluginFinder.FindAssembliesWithPlugins(pluginPath))
        {
            var hostLogger = _serviceProvider.GetRequiredService<ILogger<PluginHost>>();

            lock (_hostsLock)
                _hosts.Add(new PluginHost(_serviceProvider, hostLogger, path));
        }

        _logger.LogInformation("Plugin Manager started with {count} plugins", _hosts.Count);
    }

    public void Dispose()
    {
        lock(_hostsLock)
        {
            foreach (var host in _hosts)
            {
                host.Dispose();
            }

            _hosts.Clear();
        }
    }

    public void ExecuteOnEachHost(Action<IPluginHost> action)
    {
        lock (_hostsLock)
        {
            Parallel.ForEach(_hosts, host =>
            {
                action(host);
            });
        }
    }

    public void ExecuteOnEachPlugin(Action<IEmpyrionPlugin> action)
    {
        ExecuteOnEachHost(host =>
        {
            foreach (var pluginRef in host.Plugins)
            {
                // TODO: Trigger plugin reload/cleanup?
                if (!pluginRef.TryGetTarget(out IEmpyrionPlugin? plugin) && plugin == null)
                    return;

                action(plugin);
            }
        });
    }
}
