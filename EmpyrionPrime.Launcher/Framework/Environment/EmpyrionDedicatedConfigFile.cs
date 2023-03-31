using EmpyrionPrime.ModFramework.Environment;
using YamlDotNet.Serialization;

namespace EmpyrionPrime.Launcher.Framework.Environment
{
    internal class EmpyrionDedicatedConfigFile
    {
        public static IEmpyrionServerConfig ReadServerConfig(string file)
        {
            var config = ReadConfigFile(file);

            return config.ServerConfig ?? throw new FormatException("File does not contain an entry for ServerConfig");
        }

        public static IEmpyrionGameConfig ReadGameConfig(string file)
        {
            var config = ReadConfigFile(file);

            return config.GameConfig ?? throw new FormatException("File does not contain an entry for GameConfig");
        }

        private static ServerConfigFile ReadConfigFile(string file)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException("ServerConfig file does not exist", file);

            var deserializer = new DeserializerBuilder()
                .IgnoreUnmatchedProperties()
                .Build();

            using var reader = new StreamReader(file);

            return deserializer.Deserialize<ServerConfigFile>(reader);
        }

        private class ServerConfigFile
        {
            public EmpyrionServerConfig? ServerConfig { get; set; }
            public EmpyrionGameConfig? GameConfig { get; set; }
        }

        #region ServerConfigFile interfaces
        private class EmpyrionGameConfig : IEmpyrionGameConfig
        {
            public string? GameName { get; set; }

            public GameMode Mode { get; set; }

            public int Seed { get; set; }

            public string? CustomScenario { get; set; }
        }

        private class EmpyrionServerConfig : IEmpyrionServerConfig
        {
            [YamlMember(Alias = "Srv_Port")]
            public int Port { get; set; }

            [YamlMember(Alias = "Srv_Name")]
            public string? Name { get; set; }

            [YamlMember(Alias = "Srv_Password")]
            public string? Password { get; set; }

            [YamlMember(Alias = "Srv_MaxPlayers")]
            public int MaxPlayers { get; set; }

            [YamlMember(Alias = "Srv_ReservePlayfields")]
            public int ReservePlayFields { get; set; }

            [YamlMember(Alias = "Srv_Description")]
            public string? Description { get; set; }

            [YamlMember(Alias = "Srv_Public")]
            public bool Public { get; set; }

            [YamlMember(Alias = "Srv_LoginMessage")]
            public string? LoginMessage { get; set; }

            [YamlMember(Alias = "Srv_StopPeriod")]
            public int StopPeriod { get; set; }

            [YamlMember(Alias = "Srv_OfficialPw")]
            public string? OfficialPw { get; set; }

            public bool DisableNAT { get; set; }

            public string AdminConfigFile { get; set; } = "adminconfig.yaml";

            [YamlMember(Alias = "Tel_Enabled")]
            public bool TelnetEnabled { get; set; }

            [YamlMember(Alias = "Tel_Port")]
            public int TelnetPort { get; set; }

            [YamlMember(Alias = "Tel_Pwd")]
            public string? TelnetPassword { get; set; }

            public string SaveDirectory { get; set; } = "Saves";

            public bool EACActive { get; set; }

            public int MaxAllowedSizeClass { get; set; }

            public AllowedBlueprints AllowedBlueprints { get; set; }

            public int HeartbeatServer { get; set; }

            public int HeartbeatClient { get; set; }

            public int LogFlags { get; set; }

            public bool DisableSteamFamilySharing { get; set; }

            public int KickPlayerWithPing { get; set; }

            public int TimeoutBootingPfServer { get; set; }

            public int ShortSessionMinutes { get; set; }

            public int ShortSessionCountToLog { get; set; }

            public int ShortSessionCountToBan { get; set; }

            public bool DisableClientCheatProcessing { get; set; }

            public int PlayerLoginParallelCount { get; set; }

            public string? PlayerLoginVipNames { get; set; }

            public int PlayerLoginFullServerQueueCount { get; set; }
        }
        #endregion
    }
}
