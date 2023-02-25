using Eleon.Modding;
using EmpyrionPrime.Launcher.Plugins;
using EmpyrionPrime.Mod;
using EmpyrionPrime.RemoteClient;
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
        private readonly IRemoteEmpyrion _remoteEmpyrion;

        public ModInterfaceManager(ILogger<ModInterfaceManager> logger, IEnumerable<IPluginHost> hosts, IRemoteEmpyrion remoteEmpyrion)
        {
            _logger = logger;
            _hosts = hosts.ToArray();
            _remoteEmpyrion = remoteEmpyrion;

            _logger.LogInformation("Interface Manager loaded with {count} hosts", _hosts.Count);

            _remoteEmpyrion.GameEventHandler += PropagateGameEvent;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var now = Environment.TickCount;
            var last = Environment.TickCount;

            while(!stoppingToken.IsCancellationRequested)
            {
                now = Environment.TickCount;
                var delta = now - last;
                last = now;

                if (delta < 33)
                    await Task.Delay(33 - delta, stoppingToken);

                ExecuteOnEachPlugin(plugin => plugin.ModInterface.Game_Update());
            }
        }

        private void PropagateGameEvent(GameEvent gameEvent)
        {
            ExecuteOnEachPlugin(plugin =>
            {
                var eleonEvent = (CmdId)gameEvent.Id;

                plugin.ModInterface.Game_Event(eleonEvent, gameEvent.SequenceNumber, gameEvent.Payload);
            });

        }

        private void ExecuteOnEachPlugin(Action<IEmpyrionPlugin> action)
        {
            Parallel.ForEach(_hosts, host =>
            {
                foreach(var pluginRef in host.Plugins)
                {
                    // TODO: Remove lost plugins, possibly reload them?
                    if (!pluginRef.TryGetTarget(out IEmpyrionPlugin? plugin) && plugin == null)
                        continue;

                    action(plugin);
                }
            });
        }
    }
}
