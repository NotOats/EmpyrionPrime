using Eleon.Modding;
using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.ModFramework.Extensions;
using Microsoft.Extensions.Logging;
using System;

namespace EmpyrionPrime.ModFramework
{
    public abstract class ModInterfaceBase : ModInterface
    {
        private readonly ILoggerFactory _loggerFactory;

        // Raw game events/data
        protected delegate void GameExitingHandler();
        protected delegate void GameStartingHandler();
        protected delegate void GameUpdateHandler();
        protected delegate void GameEventHandler(CmdId eventId, ushort sequenceNumber, object data);

        protected event GameExitingHandler GameExiting;
        protected event GameStartingHandler GameStarting;
        protected event GameUpdateHandler GameUpdate;
        protected event GameEventHandler GameEvent;

        protected ModGameAPI ModGameAPI { get; private set; }

        // Addons
        protected ILogger Logger { get; }
        protected RequestBroker RequestBroker { get; private set; }
        protected ApiEvents ApiEvents { get; private set; }
        protected ApiRequests ApiRequests { get; private set; }


        protected ModInterfaceBase(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            Logger = _loggerFactory.CreateLogger(GetType());
        }

        #region ModInterface
        public void Game_Event(CmdId eventId, ushort seqNr, object data)
        {
            GameEvent?.Invoke(eventId, seqNr, data);
        }

        public void Game_Exit()
        {
            GameExiting?.Invoke();
        }

        public void Game_Start(ModGameAPI dediAPI)
        {
            ModGameAPI = dediAPI;

            RequestBroker = new RequestBroker(_loggerFactory.CreateLogger(GetType(), typeof(RequestBroker)), ModGameAPI);
            ApiEvents = new ApiEvents(_loggerFactory.CreateLogger(GetType(), typeof(ApiEvents)));
            ApiRequests = new ApiRequests(_loggerFactory.CreateLogger(GetType(), typeof(ApiRequests)), RequestBroker);

            GameEvent += (eventId, seqNr, data) =>
            {
                RequestBroker?.HandleGameEvent(eventId, seqNr, data);
                ApiEvents?.HandleGameEvent(eventId, seqNr, data);
            };

            GameStarting?.Invoke();
        }

        public void Game_Update()
        {
            GameUpdate?.Invoke();
        }
        #endregion
    }
}
