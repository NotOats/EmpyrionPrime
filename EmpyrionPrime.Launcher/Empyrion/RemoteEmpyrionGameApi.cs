using Eleon.Modding;
using EmpyrionPrime.Plugin;
using EmpyrionPrime.RemoteClient;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class RemoteEmpyrionGameApi : IEmpyrionGameApi
{
    private readonly ILogger _logger;
    private readonly IRemoteEmpyrion _remoteEmpyrion;

    public ModGameAPI ModGameAPI { get; }

    public RemoteEmpyrionGameApi(ILogger logger, IRemoteEmpyrion remoteEmpyrion)
    {
        _logger = logger;
        _remoteEmpyrion = remoteEmpyrion;

        ModGameAPI = new RemoteModGameApi(_logger, _remoteEmpyrion);
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
