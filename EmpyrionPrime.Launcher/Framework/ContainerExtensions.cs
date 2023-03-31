﻿using Eleon.Modding;
using EmpyrionPrime.Launcher.Empyrion;
using EmpyrionPrime.Launcher.Framework.Configuration;
using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.ModFramework.Configuration;
using EmpyrionPrime.ModFramework;
using EmpyrionPrime.Plugin;
using EmpyrionPrime.RemoteClient;
using SimpleInjector;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Framework
{
    internal static class ContainerExtensions
    {
        public static void RegisterEmpyrionApis(this Container container, Type pluginType)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (pluginType == null) throw new ArgumentNullException(nameof(pluginType));
            if (!pluginType.IsAssignableTo(typeof(IEmpyrionPlugin)))
                throw new ArgumentException("pluginType must be an IEmpyrionPlugin", nameof(pluginType));

            container.RegisterSingleton<IEmpyrionApiFactory, EmpyrionApiFactory>();
            container.RegisterGameApi((logger, remote) => remote.CreateBasicApi(logger));
            container.RegisterGameApi((logger, remote) => remote.CreateExtendedApi(logger));
            container.RegisterGameApi<ModGameAPI>((logger, remote) => new RemoteModGameApi(logger, remote));
        }

        private static void RegisterGameApi<TGameApi>(this Container container, Func<ILogger, IRemoteEmpyrion, TGameApi> factory)
            where TGameApi : class
        {
            container.RegisterSingleton(() =>
            {
                var logger = container.GetInstance<ILogger>();
                var remote = container.GetInstance<IRemoteEmpyrion>();

                return factory(logger, remote);
            });

        }

        public static void RegisterFrameworkApis(this Container container, Type pluginType)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (pluginType == null) throw new ArgumentNullException(nameof(pluginType));
            if (!pluginType.IsAssignableTo(typeof(IEmpyrionPlugin)))
                throw new ArgumentException("pluginType must be an IEmpyrionPlugin", nameof(pluginType));

            container.RegisterSingleton<IRequestBroker, RequestBroker>();
            container.RegisterSingleton<IApiEvents, ApiEvents>();
            container.RegisterSingleton<IApiRequests, ApiRequests>();
            container.RegisterGameApiGeneric<IPluginOptionsFactory>(typeof(PluginOptionsFactory<>), pluginType);
        }

        private static void RegisterGameApiGeneric<TInterface>(this Container container, Type genericType, Type pluginType)
            where TInterface : class
        {
            container.RegisterSingleton(() =>
            {
                var type = genericType.MakeGenericType(pluginType);
                return (TInterface)ActivatorUtilities.CreateInstance(container, type);
            });
        }
    }
}