using EmpyrionPrime.Mod;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpyrionPrime.Launcher.Plugins
{
    internal class PluginHost : IPluginHost
    {
        private readonly string _assemblyPath;
        private readonly List<IEmpyrionPlugin> _plugins = new();
        private readonly PluginLoadContext _context;
        private readonly ILogger<PluginHost> _logger;

        private bool _disposed = false;

        public string AssemblyName => Path.GetFileNameWithoutExtension(_assemblyPath);

        public IReadOnlyCollection<WeakReference<IEmpyrionPlugin>> Plugins
        {
            get { return _plugins.Select(p => new WeakReference<IEmpyrionPlugin>(p, false)).ToArray(); }
        }

        public PluginHost(IServiceProvider provider, ILogger<PluginHost> logger, string assemblyPath)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            if (string.IsNullOrEmpty(assemblyPath))
                throw new ArgumentNullException(nameof(assemblyPath));

            if (!File.Exists(assemblyPath))
                throw new FileNotFoundException("Invalid assembly path", assemblyPath);

            _assemblyPath = assemblyPath;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = new PluginLoadContext(_assemblyPath);

            LoadPlugins(provider);
        }

        public void Dispose()
        {
            if(!_disposed)
            {
                foreach(var plugin in _plugins)
                {
                    if (plugin is IDisposable disposable)
                        disposable.Dispose();
                }

                _context.Unload();

                _disposed = true;
            }
        }

        private void LoadPlugins(IServiceProvider provider)
        {
            _logger.LogInformation($"Loading plugins for {AssemblyName}");

            var assembly = _context.LoadFromAssemblyPath(_assemblyPath);
            foreach (var pluginType in PluginFinder.GetPluginTypes(assembly))
            {
                var instance = (IEmpyrionPlugin)ActivatorUtilities.CreateInstance(provider, pluginType);

                if (instance == null)
                    continue;

                _logger.LogInformation($"Loaded {pluginType}");
                _plugins.Add(instance);
            }
        }
    }
}
