using Eleon.Modding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpyrionPrime.ExamplePlugin
{
    internal class ExampleModInterface : ModInterface
    {
        private readonly ILogger _logger;
        private ModGameAPI? _gameApi;

        public ExampleModInterface(ILogger logger)
        {
            _logger = logger;
        }

        public void Game_Event(CmdId eventId, ushort seqNr, object data)
        {
            _logger.LogInformation("Game_Event(eventId: {eventId}, seqNr: {seqNr}, data.Type: {data}", eventId, seqNr, data.GetType());
        }

        public void Game_Exit()
        {
        }

        public void Game_Start(ModGameAPI dediAPI)
        {
            _gameApi = dediAPI;
        }

        public void Game_Update()
        {
        }
    }
}
