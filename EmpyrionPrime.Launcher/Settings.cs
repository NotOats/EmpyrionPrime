namespace EmpyrionPrime.Launcher;

internal class Settings
{
    public required EmpyrionSettings Empyrion { get; set; }
    public required PluginsSettings Plugins { get; set; }

}

internal class EmpyrionSettings
{
    public required string ServerPath { get; set; }
    public string DedicatedFile { get; set; } = "dedicated.yaml";

    public string EpmAddress { get; set; } = "127.0.0.1";
    public int EpmPort { get; set; } = 12345;
    public int EpmClientId { get; set; } = Environment.ProcessId;
}

internal class PluginsSettings
{
    public string Folder { get; set; } = "Plugins";

    public int GameUpdateTps { get; set; } = 20;

    public bool AutoReload { get; set; } = true;
}