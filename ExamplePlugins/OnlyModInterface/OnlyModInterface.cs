using Eleon.Modding;

namespace OnlyModInterface
{
    // Using a ModInterface instead of IEmpyrionPlugin, while not preferred, does still work.
    // This allows for hosting existing 3rd party or other legacy mods.
    public class OnlyModInterface : ModInterface
    {
        public void Game_Event(CmdId eventId, ushort seqNr, object data)
        {
        }

        public void Game_Exit()
        {
        }

        public void Game_Start(ModGameAPI dediAPI)
        {
            dediAPI.Console_Write("OnlyModInterface loaded & ready to do something!");
        }

        public void Game_Update()
        {
        }
    }
}
