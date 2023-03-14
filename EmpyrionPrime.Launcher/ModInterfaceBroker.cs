using Eleon.Modding;
using EmpyrionPrime.Launcher.Plugins;
using EmpyrionPrime.Plugin;
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
    private readonly IEmpyrionGameApiFactory _empyrionGameApiFactory;

    private readonly int _targetUpdateTps;

    private bool _disposed = false;

    public ModInterfaceBroker(
        ILogger<ModInterfaceBroker> logger,
        IOptions<PluginsSettings> settings,
        IPluginManager pluginManager, 
        IRemoteEmpyrion remoteEmpyrion,
        IEmpyrionGameApiFactory empyrionGameApiFactory)
    {
        _logger = logger;
        _pluginManager = pluginManager;
        _remoteEmpyrion = remoteEmpyrion;
        _empyrionGameApiFactory = empyrionGameApiFactory;
        _targetUpdateTps = settings.Value.GameUpdateTps;

        // Start each plugin
        _pluginManager.ExecuteOnEachPlugin(plugin =>
        {
            var gameApi = _empyrionGameApiFactory.CreateGameApi(plugin.GetType());

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
