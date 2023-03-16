using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;
using System;

namespace EmpyrionPrime.ModFramework.Extensions
{
    public static class LoggerFactoryExtensions
    {
        public static ILogger CreateLogger(this ILoggerFactory factory, string plugin, string subcategory)
        {
            return factory.CreateLogger($"{plugin}:{subcategory}");
        }

        public static ILogger CreateLogger<TPlugin>(this ILoggerFactory factory, string subcategory) where TPlugin : IEmpyrionPlugin
        {
            return CreateLogger(factory, typeof(TPlugin).Name, subcategory);
        }

        public static ILogger CreateLogger(this ILoggerFactory factory, Type plugin, Type subcategoryType)
        {
            return CreateLogger(factory, plugin.Name, subcategoryType.Name);
        }

        public static ILogger CreateLogger<TPlugin,TSubcategory>(this ILoggerFactory factory) where TPlugin : IEmpyrionPlugin
        {
            return CreateLogger(factory, typeof(TPlugin), typeof(TSubcategory));
        }
    }

}
