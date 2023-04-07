namespace EmpyrionPrime.Launcher;

internal class LauncherSettings : ISettingsValidator
{
    public required string ServerPath { get; set; }
    public string DedicatedFile { get; set; } = "dedicated.yaml";

    public string RemoteClient { get; set; } = "EpmClientAsync";

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

        // TODO: Validate RemoteClient against available clients in EmpyrionPrime.RemoteClient
        // For now ensure it's "EpmClientAsync"
        if(RemoteClient != "EpmClientAsync")
            throw new ArgumentException("Invalid remote client selection", nameof(RemoteClient));
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