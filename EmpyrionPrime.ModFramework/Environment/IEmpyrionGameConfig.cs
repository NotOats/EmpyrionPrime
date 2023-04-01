namespace EmpyrionPrime.ModFramework.Environment
{
    public interface IEmpyrionGameConfig
    {
        /// <summary>
        /// Determines the name of the save game
        /// Default: DediGame
        /// </summary>
        string GameName { get; }

        /// <summary>
        /// The server's game mode
        /// Default: GameMode.Survival
        /// </summary>
        GameMode Mode { get; }

        /// <summary>
        /// Seed used when generating the save
        /// Default: 1011345
        /// Note: This doesn't seem to work on all planets/systems
        /// </summary>
        int Seed { get; }

        /// <summary>
        /// The name of The custom scenario in the "Content\Scenarios" folder.
        /// Default: "Default Multiplayer"
        /// </summary>
        string CustomScenario { get; }
    }
}
