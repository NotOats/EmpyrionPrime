using Eleon;
using Eleon.Modding;

namespace BasicPlugin;

internal class ExampleModInterface : ModInterface
{
    private ModGameAPI? _gameApi;

    public ExampleModInterface()
    {
    }

    public void Game_Event(CmdId eventId, ushort seqNr, object data)
    {
        _gameApi?.Console_Write($"Game_Event(eventId: {eventId}, seqNr: {seqNr}, data.Type: {data?.GetType()})");

        // Weird EPM custom event for chat since they don't send regular Event_ChatMessage commands
        if((int)eventId == 201)
        {
            var message = data as MessageData;
            _gameApi?.Console_Write($"Chat message - [{message?.Channel}] E<{message?.SenderEntityId}>: {message?.Text}");
        }
    }

    public void Game_Exit()
    {
        _gameApi?.Console_Write("Game_Exit()");
    }

    public void Game_Start(ModGameAPI dediAPI)
    {
        _gameApi?.Console_Write($"Game_Start(dediApi: {dediAPI})");

        _gameApi = dediAPI;
        _gameApi?.Console_Write("Test console message");
    }

    public void Game_Update()
    {
    }
}
