using Eleon.Modding;
using EmpyrionPrime.ModFramework.Environment;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace FrameworkPlugins;

public class EmpyrionEnvironmentExample : IEmpyrionPlugin
{
    public string Name => "Empyrion Environment";
    public string Author => "NotOats";
    public Version Version => new("1.0");
    public ModInterface? ModInterface => null;

    public EmpyrionEnvironmentExample(ILogger logger, 
        // Get the entire environment
        IEmpyrionEnvironment  environment,
        
        // Use environment components directly
        IEmpyrionServerConfig serverConfig,
        IEmpyrionGameConfig   gameConfig,
        IEmpyrionAdminConfig  adminConfig)
    {
        if (serverConfig == null) throw new ArgumentNullException(nameof(serverConfig));
        if (gameConfig == null) throw new ArgumentNullException(nameof(gameConfig));
        if (adminConfig == null) throw new ArgumentNullException(nameof(adminConfig));

        LogProperties(logger, environment, nameof(IEmpyrionEnvironment));
        LogProperties(logger, environment.ServerConfig, nameof(IEmpyrionServerConfig));
        LogProperties(logger, environment.GameConfig, nameof(IEmpyrionGameConfig));

        foreach(var kvp in adminConfig.Permissions)
            logger.LogDebug("Elevated - User: {User}, Level: {PermissionLevel}", kvp.Key, kvp.Value);

        foreach (var kvp in adminConfig.BannedUsers)
            logger.LogDebug("Banned   - User: {User}, Until: {UnbanDate}", kvp.Key, kvp.Value);
    }

    private static void LogProperties(ILogger logger, object obj, string? objName = null)
    {
        objName ??= obj.GetType().Name;

        var properties = obj.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(prop => !prop.PropertyType.IsInterface);

        foreach (var prop in properties)
        {
            var value = prop.GetValue(obj, null);
            if (value == default || (value is int num && num == 0))
                continue;

            logger.LogDebug("{ObjectName}.{PropertyName}: {PropertyValue}",
                objName, prop.Name, value);
        }
    }
}
