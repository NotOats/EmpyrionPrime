using EmpyrionPrime.Launcher.Plugins;
using EmpyrionPrime.RemoteClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmpyrionPrime.Launcher;

internal class ModInterfaceBroker : BackgroundService
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger<ModInterfaceBroker> _logger;
    private readonly IPluginManager _pluginManager;
    private readonly IRemoteEmpyrion _remoteEmpyrion;

    private readonly int _targetUpdateTps;

    private bool _disposed = false;

    public ModInterfaceBroker(
        ILoggerFactory loggerFactory,
        PluginsSettings settings,
        IPluginManager pluginManager, 
        IRemoteEmpyrion remoteEmpyrion)
    {
        _loggerFactory = loggerFactory;
        _logger = _loggerFactory.CreateLogger<ModInterfaceBroker>();
        _pluginManager = pluginManager;
        _remoteEmpyrion = remoteEmpyrion;
        _targetUpdateTps = settings.GameUpdateTps;

        // Start each plugin
        _pluginManager.ExecuteOnEachPlugin(plugin =>
        {
            if (plugin.ModInterface == null)
                return;

            var logger = PluginLogger.CreateMain(_loggerFactory, plugin.GetType());
            var modGameApi = new RemoteModGameApi(logger, _remoteEmpyrion);

            plugin.ModInterface?.Game_Start(modGameApi);
        });

        _remoteEmpyrion.GameEventHandler += PropagateGameEvent;
    }

    public override void Dispose()
    {
        if (_disposed)
            return;

        // Calls the cancellation token
        base.Dispose();

        // Remove event handler and call exit on each plugin
        _remoteEmpyrion.GameEventHandler -= PropagateGameEvent;
        _pluginManager.ExecuteOnEachPlugin(plugin =>
        {
            plugin.ModInterface?.Game_Exit();
        });

        _disposed = true;
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

            _pluginManager.ExecuteOnEachPlugin(plugin => plugin.ModInterface?.Game_Update());
        }
    }

    private void PropagateGameEvent(GameEvent gameEvent)
    {
        _pluginManager.ExecuteOnEachPlugin(plugin =>
        {
            var eleonEvent = gameEvent.Id;

            plugin.ModInterface?.Game_Event(eleonEvent, gameEvent.SequenceNumber, gameEvent.Payload);
        });

    }
}
