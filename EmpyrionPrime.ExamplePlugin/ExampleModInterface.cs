using Eleon;
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
            _logger.LogInformation("Game_Event(eventId: {eventId}, seqNr: {seqNr}, data.Type: {data})", eventId, seqNr, data?.GetType());

            // Weird EPM custom event for chat since they don't send regular Event_ChatMessage commands
            if((int)eventId == 201)
            {
                var message = data as MessageData;
                _logger.LogInformation("Chat message - [{Channel}] E<{Id}>: {Text}", message?.Channel, message?.SenderEntityId, message?.Text);
            }
        }

        public void Game_Exit()
        {
            _logger.LogInformation("Game_Exit()");
        }

        public void Game_Start(ModGameAPI dediAPI)
        {
            _logger.LogInformation("Game_Start(dediApi: {ModGameApi})", dediAPI);

            _gameApi = dediAPI;
            _gameApi.Console_Write("ExampleModInterface: Test console message");
        }

        public void Game_Update()
        {
        }
    }
}
