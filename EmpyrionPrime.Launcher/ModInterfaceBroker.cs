using Eleon.Modding;
using EmpyrionPrime.Launcher.Empyrion;
using EmpyrionPrime.Launcher.Plugins;
using EmpyrionPrime.Mod;
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
        ILoggerFactory logger,
        IOptions<PluginsSettings> settings,
        IPluginManager pluginManager, 
        IRemoteEmpyrion remoteEmpyrion)
    {
        _loggerFactory = logger;
        _logger = logger.CreateLogger<ModInterfaceBroker>();
        _pluginManager = pluginManager;
        _remoteEmpyrion = remoteEmpyrion;
        _targetUpdateTps = settings.Value.GameUpdateTps;

        // Start each plugin
        _pluginManager.ExecuteOnEachPlugin(plugin =>
        {
            var pluginLogger = _loggerFactory.CreateLogger(plugin.GetType());
            var gameApi = new EmpyrionGameApi(pluginLogger, _remoteEmpyrion);

            plugin.ModInterface.Game_Start(gameApi.ModGameAPI);
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
            plugin.ModInterface.Game_Exit();
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
