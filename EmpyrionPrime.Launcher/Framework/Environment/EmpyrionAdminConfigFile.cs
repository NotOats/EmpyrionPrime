using EmpyrionPrime.ModFramework.Environment;
using YamlDotNet.Serialization;

namespace EmpyrionPrime.Launcher.Framework.Environment;

internal class EmpyrionAdminConfigFile : IEmpyrionAdminConfig
{
    public IReadOnlyDictionary<long, int> Permissions { get; }

    public IReadOnlyDictionary<long, DateTime> BannedUsers { get; }

    private EmpyrionAdminConfigFile(AdminConfigFile? configFile)
    {
        Permissions = configFile?.Elevated?.ToDictionary(x => x.Id, x => x.Permission) 
            ?? new Dictionary<long, int>();

        BannedUsers = configFile?.Banned?.ToDictionary(x => x.Id, x => x.Until) 
            ?? new Dictionary<long, DateTime>();
    }

    public static EmpyrionAdminConfigFile ReadAdminConfig(string file)
    {
        if (!File.Exists(file))
            // TODO: Pin down if blank config is best, can a server run without an adminconfig.yaml?
            //throw new FileNotFoundException("AdminConfig file does not exist", file);
            return new EmpyrionAdminConfigFile(null);

        var deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .Build();

        using var reader = new StreamReader(file);

        var configFile = deserializer.Deserialize<AdminConfigFile>(reader) 
            ?? throw new FormatException("Failed to read AdminConfigFile");

        return new EmpyrionAdminConfigFile(configFile);
    }

    #region AdminConfig Parsing
    private class AdminConfigFile
    {
        public List<UserPermission>? Elevated { get; set; }
        public List<UserBanned>? Banned { get; set; }
    }

    private class UserPermission
    {
        public long Id { get; set; }
        public int Permission { get; set; }
    }

    private class UserBanned
    {
        public long Id { get; set; }
        public DateTime Until { get; set; }
    }
    #endregion
}
