using System.Net;

namespace EmpyrionPrime.Launcher;

internal class Settings
{
    public required EmpyrionSettings Empyrion { get; set; }
    public required PluginsSettings Plugins { get; set; }

}

internal class EmpyrionSettings : ISettingsValidator
{
    public required string ServerPath { get; set; }
    public string DedicatedFile { get; set; } = "dedicated.yaml";

    public string EpmAddress { get; set; } = "127.0.0.1";
    public int EpmPort { get; set; } = 12345;
    public int EpmClientId { get; set; } = Environment.ProcessId;

    public void Validate()
    {
        // Server Path
        if (ServerPath == null)
            throw new ArgumentNullException(nameof(ServerPath));

        if (!Directory.Exists(ServerPath))
            throw new DirectoryNotFoundException("ServerPath was not found");

        // Dedicated File
        var computed = Path.Combine(ServerPath, DedicatedFile);
        if (!File.Exists(DedicatedFile) && !File.Exists(computed))
            throw new FileNotFoundException("DedicatedFile was not found", DedicatedFile);

        // EpmAddress
        if (!IPAddress.TryParse(EpmAddress, out _))
            throw new FormatException("EpmAddress is not a valid IP address");

        // EpmPort
        if (EpmPort < 1 || EpmPort > 65535)
            throw new FormatException("EpmPort is not a valid port");
    }
}

internal class PluginsSettings : ISettingsValidator
{
    public string Folder { get; set; } = "Plugins";

    public int GameUpdateTps { get; set; } = 20;

    public bool AutoReload { get; set; } = true;

    public void Validate()
    {
        // GameUpdateTps
        if (GameUpdateTps < 1)
            throw new FormatException("GameUpdateTps can not be less than 1");
    }
}

internal interface ISettingsValidator
{
    void Validate();
}