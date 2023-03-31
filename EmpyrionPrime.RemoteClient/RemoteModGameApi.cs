using Eleon.Modding;
using Microsoft.Extensions.Logging;
using System;

namespace EmpyrionPrime.RemoteClient
{
    public class RemoteModGameApi : ModGameAPI
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
            _remoteEmpyrion.SendRequest(reqId, seqNr, data);

            return true;
        }
    }
}
