using EmpyrionPrime.ModFramework.Environment;
using System.Runtime.CompilerServices;

namespace EmpyrionPrime.Launcher.Framework.Environment;

internal class EmpyrionEnvironment : IEmpyrionEnvironment
{
    public string ServerDirectory { get; }

    public string ScenarioDirectory { get; }

    public string DefaultContentDirectory { get; }

    public string ScenarioContentDirectory { get; }

    public string BaseSaveGameDirectory { get; }

    public string CurrentSaveGameDirectory { get; }

    public IEmpyrionServerConfig ServerConfig { get; }

    public IEmpyrionGameConfig GameConfig { get; }

    public IEmpyrionAdminConfig AdminConfig { get; }

    public EmpyrionEnvironment(EmpyrionSettings settings)
    {
        ServerDirectory = settings.ServerPath ?? throw new ArgumentNullException(nameof(settings), "ServerPath is null");
        ThrowIfNotExists(ServerDirectory);

        // Load Server & Game configs
        var dedicatedFile = FindDedicatedFile(settings);
        ServerConfig = EmpyrionDedicatedConfigFile.ReadServerConfig(dedicatedFile);
        GameConfig = EmpyrionDedicatedConfigFile.ReadGameConfig(dedicatedFile);

        // Load the rest of the directory paths
        ScenarioDirectory = Path.Combine(ServerDirectory, @"Content\Scenarios", GameConfig.CustomScenario);
        ThrowIfNotExists(ScenarioDirectory);

        DefaultContentDirectory = Path.Combine(ServerDirectory, "Content");
        ThrowIfNotExists(DefaultContentDirectory);

        ScenarioContentDirectory = Path.Combine(ScenarioDirectory, "Content");
        ThrowIfNotExists(ScenarioContentDirectory);

        BaseSaveGameDirectory = Path.Combine(ServerDirectory, ServerConfig.SaveDirectory);
        ThrowIfNotExists(BaseSaveGameDirectory);

        CurrentSaveGameDirectory = Path.Combine(BaseSaveGameDirectory, "Games", GameConfig.GameName);
        ThrowIfNotExists(CurrentSaveGameDirectory);

        var adminConfigFile = Path.Combine(BaseSaveGameDirectory, ServerConfig.AdminConfigFile);
        AdminConfig = EmpyrionAdminConfigFile.ReadAdminConfig(adminConfigFile);
    }

    private static string FindDedicatedFile(EmpyrionSettings settings)
    {
        var file = settings.DedicatedFile;

        // Configuration file has a full file path
        if (Path.IsPathRooted(file) && File.Exists(file))
            return file;

        var computed = Path.Join(settings.ServerPath, file);
        if (File.Exists(computed))
            return computed;

        throw new FileNotFoundException("Failed to find server dedicated config file", file);
    }

    private static void ThrowIfNotExists(string directory, [CallerArgumentExpression(nameof(directory))] string variableName = "")
    {
        if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            throw new DirectoryNotFoundException($"{variableName} was not found");
    }
}
