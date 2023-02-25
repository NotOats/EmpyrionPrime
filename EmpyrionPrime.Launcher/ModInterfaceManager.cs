using EmpyrionPrime.Launcher.Plugins;
using EmpyrionPrime.Mod;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpyrionPrime.Launcher
{
    internal class ModInterfaceManager : BackgroundService
    {
        private readonly ILogger<ModInterfaceManager> _logger;
        private readonly IReadOnlyList<IPluginHost> _hosts;

        public ModInterfaceManager(ILogger<ModInterfaceManager> logger, IEnumerable<IPluginHost> hosts)
        {
            _logger = logger;
            _hosts = hosts.ToArray();

            _logger.LogInformation("Interace Manager loaded with {count} hosts", _hosts.Count);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ConfigurePluginHosts();

            var now = Environment.TickCount;
            var last = Environment.TickCount;

            while(!stoppingToken.IsCancellationRequested)
            {
                now = Environment.TickCount;
                var delta = now - last;
                last = now;

                if (delta < 33)
                    await Task.Delay(33 - delta, stoppingToken);

                Parallel.ForEach(_hosts, host =>
                {
                    foreach(var pluginRef in host.Plugins)
                    {
                        // TODO: Remove lost plugins, possibly reload them?
                        if (!pluginRef.TryGetTarget(out IEmpyrionPlugin? plugin) && plugin == null)
                            continue;

                        plugin.ModInterface.Game_Update();
                    }
                });
            }
        }

        private Task ConfigurePluginHosts()
        {
            return Task.CompletedTask;
        }
    }
}
