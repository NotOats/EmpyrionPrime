namespace EmpyrionPrime.ModFramework.Environment
{
    public interface IEmpyrionEnvironment
    {
        #region Directories
        /// <summary>
        /// The Empyrion server's directory
        /// Note: Typically where "BuildNumber.txt" is located.
        /// </summary>
        string ServerDirectory { get; }

        /// <summary>
        /// Directory of the currently running custom scenario or null
        /// Note: Typically at "${ServerDirectory}\Content\Scenarios\${GameConfig.CustomScenario}
        /// </summary>
        string ScenarioDirectory { get; }

        /// <summary>
        /// The server's default content directory
        /// Note: Typically at "${ServerDirectory}\Content"
        /// </summary>
        string DefaultContentDirectory { get; }

        /// <summary>
        /// The content directory for the currently running scenario or null
        /// Note: Typically at "${ServerDirectory}\Content\Scenarios\${GameConfig.CustomScenario}\Content
        /// </summary>
        string ScenarioContentDirectory { get; }

        /// <summary>
        /// The server's base save game directory
        /// Note: Typically at "${ServerDirectory}\${ServerConfig.SaveDirectory}"
        /// </summary>
        string BaseSaveGameDirectory { get; }

        /// <summary>
        /// The server's current save game directory
        /// Note: Typically at "${ServerDirectory}\${ServerConfig.SaveDirectory}\Games\${GameConfig.GameName}
        /// </summary>
        string CurrentSaveGameDirectory { get; }
        #endregion

        #region File Wrappers
        /// <summary>
        /// The server's configuration settings, found in dedicated.yaml under the "ServerConfig" section
        /// </summary>
        IEmpyrionServerConfig ServerConfig { get; }

        /// <summary>
        /// The server's game config settings, found in dedicated.yaml under the "GameConfig" section
        /// </summary>
        IEmpyrionGameConfig GameConfig { get; }

        /// <summary>
        /// The server's admin config file, typically found in adminconfig.yaml under the Saves directory
        /// </summary>
        IEmpyrionAdminConfig AdminConfig { get; }
        #endregion
    }
}
