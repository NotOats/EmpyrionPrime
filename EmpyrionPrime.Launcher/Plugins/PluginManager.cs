using EmpyrionPrime.Launcher.Extensions;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace EmpyrionPrime.Launcher.Plugins;

internal interface IPluginManager : IDisposable
{
    IEnumerable<IPluginHost> Hosts { get; }

    void ExecuteOnEachHost(Action<IPluginHost> action);
    void ExecuteOnEachPlugin(Action<IEmpyrionPlugin> action);
}

internal class PluginManager : IPluginManager
{
    private readonly ILogger<PluginManager> _logger;
    private readonly PluginsSettings _settings;
    private readonly IServiceProvider _serviceProvider;

    private readonly object _hostsLock = new();
    private readonly List<PluginHost> _hosts = new();

    private int _disposeCount = 0;

    public IEnumerable<IPluginHost> Hosts
    {
        get
        {
            ThrowIfDisposed(); 
            return _hosts.AsLocked(_hostsLock);
        }
    }

    public string PluginFolder => Path.Join(Environment.CurrentDirectory, _settings.Folder);

    public PluginManager(ILogger<PluginManager> logger, PluginsSettings settings, IServiceProvider provider) 
    {
        _logger = logger;
        _settings = settings;
        _serviceProvider = provider;

        LoadPlugins();
    }

    public void Dispose()
    {
        if (Interlocked.Increment(ref _disposeCount) != 1)
            return;

        UnloadPlugins();
    }

    public void ExecuteOnEachHost(Action<IPluginHost> action)
    {
        ThrowIfDisposed();

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
        ThrowIfDisposed();

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

    private void LoadPlugins()
    {
        if (!Directory.Exists(PluginFolder))
        {
            _logger.LogWarning("Plugin Path '{PluginPath}' does not exist", PluginFolder);
            return;
        }

        foreach (var path in PluginFinder.FindAssembliesWithPlugins(PluginFolder))
        {
            var hostLogger = _serviceProvider.GetRequiredService<ILogger<PluginHost>>();

            lock (_hostsLock)
                _hosts.Add(new PluginHost(_serviceProvider, hostLogger, path));
        }

        _logger.LogInformation("Plugin Manager loaded {count} plugins", _hosts.Count);
    }

    private void UnloadPlugins()
    {
        lock(_hostsLock)
        {
            _logger.LogDebug("Unloading plugins");
            var count = _hosts.Count;
            foreach (var host in _hosts)
            {
                host.Dispose();
            }

            _hosts.Clear();
            _logger.LogInformation("Unloaded {PluginCount} plugins", count);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ThrowIfDisposed()
    {
        if (_disposeCount > 0) throw new ObjectDisposedException(GetType().Name);
    }
}
