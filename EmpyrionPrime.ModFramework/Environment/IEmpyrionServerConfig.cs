namespace EmpyrionPrime.ModFramework.Environment
{
    public interface IEmpyrionServerConfig
    {
        /// <summary>
        /// The port number Empyrion listens on
        /// Default: 30000
        /// </summary>
        int Port { get; }

        /// <summary>
        /// The server's name
        /// Default: My Server
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The password required to connect
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Maximum number of connected players
        /// Default: 8
        /// </summary>
        int MaxPlayers { get; }

        /// <summary>
        /// Number of idle playefield servers held in reserve
        /// Default: 1
        /// </summary>
        int ReservePlayFields { get; }

        /// <summary>
        /// The server's description
        /// Note: Maximum 127 characters.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Determines if the server will show up on the public in-game server browser.
        /// </summary>
        bool Public { get; }

        string LoginMessage { get; }

        /// <summary>
        /// How often, in real time hours, to stop all playfield servers
        /// Note: Players will recieve a warning message before this happens
        /// </summary>
        int StopPeriod { get; }

        string OfficialPw { get; }

        bool DisableNAT { get; }

        /// <summary>
        /// Name of the Admin Config File
        /// Default: adminconfig.yaml
        /// </summary>
        string AdminConfigFile { get; } // TODO: Add interface to IEmpyrionEnvironment for this

        /// <summary>
        /// Determines if telnet is enabled
        /// </summary>
        bool TelnetEnabled { get; }

        /// <summary>
        /// The port for telnet access
        /// Default: 30004
        /// </summary>
        int TelnetPort { get; }

        /// <summary>
        /// The password required to connect to telnet
        /// </summary>
        string TelnetPassword { get; }

        /// <summary>
        /// Where the server will put savegames
        /// Default: Saves
        /// </summary>
        string SaveDirectory { get; }

        /// <summary>
        /// Determines if the server will use Easy Anti-Cheat
        /// Default: true
        /// </summary>
        bool EACActive { get; }

        /// <summary>
        /// Maximum allowed size class of spawned blueprints
        /// Formula: [DeviceFactor (device count x 0.01) + LightFactor (lights count x 0.05) + TrianglesFactor (triangle count x 0.00027)] ÷ 3
        /// </summary>
        int MaxAllowedSizeClass { get; }

        /// <summary>
        /// Which blueprints are allowed to be spawned
        /// </summary>
        AllowedBlueprints AllowedBlueprints { get; }

        /// <summary>
        /// Timeout in seconds after which a playfield server will be killed if it does not respond
        /// Default: 15
        /// Note: Set to 0 to disable
        /// </summary>
        int HeartbeatServer { get; }

        /// <summary>
        /// Timeout in seconds after which a client will be disconnected if it does not respond
        /// Default: 30
        /// Note: Set to 0 to disable
        /// </summary>
        int HeartbeatClient { get; }

        /// <summary>
        /// Enables extra log output for debug purposes
        /// Default: 0
        /// Note: 0 = None, 1 = EAC, 2 = EAC_All_Details
        /// </summary>
        int LogFlags { get; }

        /// <summary>
        /// Disabled Steam Family Share, only allows the game owner to play
        /// Default: False
        /// </summary>
        bool DisableSteamFamilySharing { get; }

        /// <summary>
        /// Players with a higher ping than the specified value will be removed from the server
        /// </summary>
        int KickPlayerWithPing { get; }

        /// <summary>
        /// Kills a playfield server if it takes longer than the specified value to start
        /// Default: 90
        /// Note: This is a conservative default, for faster servers 60 should should be ok
        /// </summary>
        int TimeoutBootingPfServer { get; }

        /// <summary>
        /// How many minutes of playtime is considered a "short session"
        /// Note: Set to 0 to disable short sessions
        /// </summary>
        int ShortSessionMinutes { get; }

        /// <summary>
        /// How many consecutive "short sessions" a client has before a warning is written to the server log
        /// Note: Set to 0 to disable short sessions
        /// </summary>
        int ShortSessionCountToLog { get; }

        /// <summary>
        /// How many consecutive "short sessions" a client has before they are banned
        /// Note: Set to 0 to disable short sessions
        /// </summary>
        int ShortSessionCountToBan { get; }

        bool DisableClientCheatProcessing { get; }

        /// <summary>
        /// The maximum number of logins that can occur at one time
        /// Default: 5
        /// </summary>
        int PlayerLoginParallelCount { get; }

        /// <summary>
        /// A comma seperates list of Steam Ids for preferred players, this will give them a better position in the login queue.
        /// Note: Format example "123456789,123456788,123456787"
        /// </summary>
        string PlayerLoginVipNames { get; }

        /// <summary>
        /// Maximum number of players that can queue to login when the PlayerLoginParallelCount has been reached
        /// </summary>
        int PlayerLoginFullServerQueueCount { get; }
    }
}
