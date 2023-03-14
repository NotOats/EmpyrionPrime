using Eleon.Modding;
using Microsoft.Extensions.Logging;
using System;

namespace EmpyrionPrime.ModFramework
{
    public abstract class BaseMod : ModInterface
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


        protected BaseMod(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            Logger = _loggerFactory.CreateLogger(GetType());
        }

        #region ModInterface
        public void Game_Event(CmdId eventId, ushort seqNr, object data)
        {
            RequestBroker?.HandleGameEvent(eventId, seqNr, data);

            GameEvent?.Invoke(eventId, seqNr, data);
        }

        public void Game_Exit()
        {
            GameExiting?.Invoke();
        }

        public void Game_Start(ModGameAPI dediAPI)
        {
            ModGameAPI = dediAPI;

            var brokerLogger = _loggerFactory.CreateLogger<RequestBroker>();
            RequestBroker = new RequestBroker(brokerLogger, ModGameAPI);

            GameStarting?.Invoke();
        }

        public void Game_Update()
        {
            GameUpdate?.Invoke();
        }
        #endregion
    }
}
