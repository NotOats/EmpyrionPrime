using Eleon.Modding;
using EmpyrionPrime.Plugin;
using EmpyrionPrime.Plugin.Api;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace EmpyrionPrime.RemoteClient.Epm.Api
{
    internal class EpmBasicEmpyrionApi : IBasicEmpyrionApi, IDisposable
    {
        private int _disposeCount = 0;

        protected IRemoteEmpyrion Client { get; }

        public event GameEventHandler GameEvent;

        public EpmBasicEmpyrionApi(ILogger logger, IRemoteEmpyrion client)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            Client = client ?? throw new ArgumentNullException(nameof(client));

            Client.GameEventHandler += HandleGameEvent;
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (Interlocked.Increment(ref _disposeCount) != 1)
                return;

            Client.GameEventHandler -= HandleGameEvent;
        }

        public void SendEvent(CmdId id, ushort sequenceNumber, object data)
        {
            Client.SendRequest(id, sequenceNumber, data);
        }

        protected virtual void HandleGameEvent(GameEvent gameEvent)
        {
            GameEvent?.Invoke(gameEvent.Id, gameEvent.SequenceNumber, gameEvent.Payload);
        }
    }
}
