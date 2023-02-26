using Eleon.Modding;
using System;

namespace EmpyrionPrime.RemoteClient.Api
{
    public class RemoteModGameApi : ModGameAPI
    {
        private readonly IRemoteEmpyrion _remoteEmpyrion;

        public event Action<string> ConsoleWriteHandler;
        public event Func<ulong> GetTickTimeHandler;

        public RemoteModGameApi(IRemoteEmpyrion remoteEmpyrion)
        {
            _remoteEmpyrion = remoteEmpyrion;
        }

        public void Console_Write(string txt)
        {
            ConsoleWriteHandler?.Invoke(txt);
        }

        public ulong Game_GetTickTime()
        {
            return GetTickTimeHandler?.Invoke() ?? 0;
        }

        public bool Game_Request(CmdId reqId, ushort seqNr, object data)
        {
            _remoteEmpyrion.SendRequest((CommandId)reqId, seqNr, data);

            return true;
        }
    }
}
