using Eleon;
using Eleon.Modding;
using EmpyrionPrime.Plugin;

namespace BasicPlugin;

public class BasicPlugin : IEmpyrionPlugin
{
    public string Name => "Basic Plugin";
    public string Author => "NotOats";
    public Version Version => new(1, 0);

    public ModInterface ModInterface { get; }

    public BasicPlugin()
    {
        ModInterface = new ExampleModInterface();
    }

    private class ExampleModInterface : ModInterface
    {
        private ModGameAPI? _modGameApi;

        public void Game_Event(CmdId eventId, ushort seqNr, object data)
        {
            _modGameApi?.Console_Write($"Game_Event(eventId: {eventId}, seqNr: {seqNr}, data.Type: {data?.GetType()})");

            // Weird EPM custom event for chat since they don't send regular Event_ChatMessage commands
            if ((int)eventId == 201)
            {
                var message = data as MessageData;
                _modGameApi?.Console_Write($"Game_Event - Chat message - [{message?.Channel}] E<{message?.SenderEntityId}>: {message?.Text}");
            }
        }

        public void Game_Exit()
        {
            _modGameApi?.Console_Write("Game_Exit()");
        }

        public void Game_Start(ModGameAPI dediAPI)
        {
            _modGameApi = dediAPI;
            _modGameApi?.Console_Write($"ModGameAPI.Game_GetTickTime(): {dediAPI.Game_GetTickTime()}");
        }

        public void Game_Update()
        {
        }
    }

}
