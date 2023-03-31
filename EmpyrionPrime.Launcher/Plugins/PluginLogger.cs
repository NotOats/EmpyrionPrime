using EmpyrionPrime.ModFramework.Extensions;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Plugins
{
    internal static class PluginLogger
    {
        public static ILogger CreateMain(ILoggerFactory factory, Type pluginType)
        {
            if (pluginType.IsGenericType && pluginType.GetGenericTypeDefinition() == typeof(ModInterfaceWrapper<>))
                pluginType = pluginType.GetGenericArguments()[0];

            return factory.CreateLogger(pluginType.Name, "Main");
        }
    }
}
