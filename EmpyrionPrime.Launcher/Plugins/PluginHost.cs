using EmpyrionPrime.Launcher.Framework;
using EmpyrionPrime.Plugin;
using EmpyrionPrime.RemoteClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleInjector;

namespace EmpyrionPrime.Launcher.Plugins;

internal interface IPluginHost : IDisposable
{
    public string AssemblyName { get; }
    public IReadOnlyCollection<WeakReference<IEmpyrionPlugin>> Plugins { get; }
}

internal class PluginHost : IPluginHost
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PluginHost> _logger;
    private readonly string _assemblyPath;

    private readonly Dictionary<IEmpyrionPlugin, Container> _plugins = new();
    private readonly PluginLoadContext _context;

    private int _disposeCount = 0;

    public string AssemblyName => Path.GetFileNameWithoutExtension(_assemblyPath);

    public IReadOnlyCollection<WeakReference<IEmpyrionPlugin>> Plugins
    {
        get { return _plugins.Select(kvp => new WeakReference<IEmpyrionPlugin>(kvp.Key, false)).ToArray(); }
    }

    public PluginHost(IServiceProvider provider, ILogger<PluginHost> logger, string assemblyPath)
    {
        if (string.IsNullOrEmpty(assemblyPath))
            throw new ArgumentNullException(nameof(assemblyPath));

        if (!File.Exists(assemblyPath))
            throw new FileNotFoundException("Invalid assembly path", assemblyPath);

        _serviceProvider = provider ?? throw new ArgumentNullException(nameof(provider));
        _assemblyPath = assemblyPath;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = new PluginLoadContext(_assemblyPath);

        LoadPlugins();
    }

    public void Dispose()
    {
        if (Interlocked.Increment(ref _disposeCount) != 1)
            return;

        foreach (var kvp in _plugins)
        {
            if (kvp.Key is IDisposable disposable)
                disposable.Dispose();

            kvp.Value.Dispose();
        }

        _context.Unload();
    }

    private void LoadPlugins()
    {
        var assembly = _context.LoadFromAssemblyPath(_assemblyPath);
        foreach (var pluginType in PluginFinder.GetPluginTypes(assembly))
        {
            var container = CreateContainer(pluginType);
            var instance = (IEmpyrionPlugin)ActivatorUtilities.CreateInstance(container, pluginType);

            if (instance == null)
            {
                container.Dispose();
                continue;
            }

            _logger.LogInformation("Loaded {AssemblyName} - {PluginName} v{Version} by {Author}", 
                AssemblyName, instance.Name, instance.Version, instance.Author);

            _plugins.Add(instance, container);
        }
    }

    private Container CreateContainer(Type pluginType)
    {
        var container = new Container();

        // Register container
        container.RegisterInstance<IServiceProvider>(container);

        // Settings & Environment
        container.RegisterInstance(_serviceProvider.GetRequiredService<LauncherSettings>());
        container.RegisterInstance(_serviceProvider.GetRequiredService<PluginsSettings>());
        container.RegisterSingleton(_serviceProvider.GetRequiredService<IHostEnvironment>);

        // Logging
        container.RegisterSingleton(_serviceProvider.GetRequiredService<ILoggerFactory>);
        //container.RegisterSingleton(typeof(ILogger<>), typeof(Logger<>));
        container.RegisterSingleton(() =>
        {
            var factory = container.GetInstance<ILoggerFactory>();
            return PluginLogger.CreateMain(factory, pluginType);
        });

        // Empyrion Apis
        container.RegisterSingleton(_serviceProvider.GetRequiredService<IRemoteEmpyrion>);
        container.RegisterEmpyrionApis(pluginType);
        container.RegisterFrameworkApis(pluginType);

#if DEBUG
        container.Verify();
#endif

        return container;
    }
}
