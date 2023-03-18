using Eleon.Modding;
using EmpyrionPrime.Launcher.Plugins;
using EmpyrionPrime.ModFramework.Extensions;
using EmpyrionPrime.Plugin;
using EmpyrionPrime.RemoteClient;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class RemoteBasicEmpyrionApi<TPlugin> : IBasicEmpyrionApi, IDisposable where TPlugin : IEmpyrionPlugin
{
    private readonly ILogger _logger;

    protected IRemoteEmpyrion RemoteEmpyrion { get; }

    public ModGameAPI ModGameAPI { get; }

    public event GameEventHandler? GameEvent;

    public RemoteBasicEmpyrionApi(ILoggerFactory loggerFactory, IRemoteEmpyrion remoteEmpyrion)
    {
        // Override for clean logging with ModInterfaceWrapper
        var pluginType = typeof(TPlugin);
        if (pluginType.IsGenericType && pluginType.GetGenericTypeDefinition() == typeof(ModInterfaceWrapper<>))
            pluginType = pluginType.GetGenericArguments()[0];

        _logger = loggerFactory?.CreateLogger(pluginType.Name, "Main") ?? throw new ArgumentNullException(nameof(loggerFactory));
        RemoteEmpyrion = remoteEmpyrion ?? throw new ArgumentNullException(nameof(remoteEmpyrion));

        ModGameAPI = new RemoteModGameApi(_logger, RemoteEmpyrion);

        RemoteEmpyrion.GameEventHandler += HandleGameEvent;
    }

    public void Dispose()
    {
        RemoteEmpyrion.GameEventHandler -= HandleGameEvent;
    }

    protected virtual void HandleGameEvent(GameEvent gameEvent)
    {
        GameEvent?.Invoke((CmdId)gameEvent.Id, gameEvent.SequenceNumber, gameEvent.Payload);
    }

    private class RemoteModGameApi : ModGameAPI
    {
        private readonly ILogger _logger;
        private readonly IRemoteEmpyrion _remoteEmpyrion;

        public RemoteModGameApi(ILogger logger, IRemoteEmpyrion remoteEmpyrion)
        {
            _logger = logger;
            _remoteEmpyrion = remoteEmpyrion;
        }

        public void Console_Write(string txt)
        {
            _logger.LogInformation("{text}", txt);
        }

        public ulong Game_GetTickTime()
        {
            return (ulong)Environment.TickCount;
        }

        public bool Game_Request(CmdId reqId, ushort seqNr, object data)
        {
            _remoteEmpyrion.SendRequest((CommandId)reqId, seqNr, data);

            return true;
        }
    }
}
