using Eleon.Modding;
using EmpyrionPrime.ModFramework.Environment;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace FrameworkPlugins;

public class EnvironmentExamplePlugin : IEmpyrionPlugin
{
    public string Name => "Environment Example Plugin";
    public string Author => "NotOats";
    public Version Version => new("1.0");
    public ModInterface? ModInterface => null;

    public EnvironmentExamplePlugin(ILogger logger, IEmpyrionEnvironment environment)
    {
        LogProperties(logger, environment, nameof(IEmpyrionEnvironment));
        LogProperties(logger, environment.ServerConfig, nameof(IEmpyrionServerConfig));
        LogProperties(logger, environment.GameConfig, nameof(IEmpyrionGameConfig));
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
