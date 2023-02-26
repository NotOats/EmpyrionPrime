using Eleon.Modding;
using EmpyrionPrime.Launcher.Plugins;
using EmpyrionPrime.Mod;
using EmpyrionPrime.RemoteClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmpyrionPrime.Launcher;

internal class ModInterfaceBroker : BackgroundService
{
    private readonly ILogger<ModInterfaceBroker> _logger;
    private readonly IPluginManager _pluginManager;
    private readonly IRemoteEmpyrion _remoteEmpyrion;
    private readonly int _targetUpdateTps;

    public ModInterfaceBroker(
        ILogger<ModInterfaceBroker> logger, 
        IPluginManager pluginManager, 
        IRemoteEmpyrion remoteEmpyrion, 
        IOptions<PluginsSettings> settings)
    {
        _logger = logger;
        _pluginManager = pluginManager;
        _remoteEmpyrion = remoteEmpyrion;
        _targetUpdateTps = settings.Value.GameUpdateTps;

        _remoteEmpyrion.GameEventHandler += PropagateGameEvent;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("ModInterface Broker started with {tps}tps target", _targetUpdateTps);

        var updateLoopDelay = 1000 / _targetUpdateTps;

        var now = Environment.TickCount;
        var last = Environment.TickCount;

        while(!stoppingToken.IsCancellationRequested)
        {
            now = Environment.TickCount;
            var delta = now - last;
            last = now;

            if (delta < updateLoopDelay)
                await Task.Delay(updateLoopDelay - delta, stoppingToken);

            _pluginManager.ExecuteOnEachPlugin(plugin => plugin.ModInterface.Game_Update());
        }
    }

    private void PropagateGameEvent(GameEvent gameEvent)
    {
        _pluginManager.ExecuteOnEachPlugin(plugin =>
        {
            var eleonEvent = (CmdId)gameEvent.Id;

            plugin.ModInterface.Game_Event(eleonEvent, gameEvent.SequenceNumber, gameEvent.Payload);
        });

    }
}
