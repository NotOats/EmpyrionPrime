﻿using EmpyrionPrime.ModFramework;
using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.Plugin;
using EmpyrionPrime.RemoteClient;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion
{
    internal class EmpyrionApiFactory<TPlugin> : IEmpyrionApiFactory<TPlugin> where TPlugin : IEmpyrionPlugin
    {
        private readonly IReadOnlyDictionary<Type, Func<IEmpyrionApiFactory<TPlugin>, IEmpyrionApi>> _apiImplementations;

        private readonly Dictionary<Type, IEmpyrionApi?> _apiCache = new();
        private readonly object _apiCacheLock = new();
        private readonly ILoggerFactory _loggerFactory;
        private readonly IRemoteEmpyrion _remoteEmpyrion;

        public EmpyrionApiFactory(ILoggerFactory loggerFactory, IRemoteEmpyrion remoteEmpyrion)
        {
            _loggerFactory = loggerFactory;
            _remoteEmpyrion = remoteEmpyrion;

            // TODO: Dynamically load IEmpyrionApi interfaces from Plugin & ModFramework libs
            _apiImplementations = new Dictionary<Type, Func<IEmpyrionApiFactory<TPlugin>, IEmpyrionApi>>
            {
                { typeof(IBasicEmpyrionApi),    apiFactory => { return new RemoteBasicEmpyrionApi<TPlugin>(_loggerFactory, _remoteEmpyrion); } },
                { typeof(IExtendedEmpyrionApi), apiFactory => { return new RemoteExtendedEmpyrionApi<TPlugin>(_loggerFactory, _remoteEmpyrion); } },
                { typeof(IRequestBroker),       apiFactory => { return new RemoteRequestBroker<TPlugin>(_loggerFactory, apiFactory); } },
                { typeof(IApiEvents),           apiFactory => { return new RemoteApiEvents<TPlugin>(_loggerFactory, apiFactory); } },
                { typeof(IApiRequests),         apiFactory => { return new RemoteApiRequests<TPlugin>(_loggerFactory, apiFactory); } }
            };
        }

        public TEmpyrionApi? Create<TEmpyrionApi>() where TEmpyrionApi : class, IEmpyrionApi
        {
            var type = typeof(TEmpyrionApi);

            lock (_apiCacheLock)
            {
                if (!_apiCache.TryGetValue(type, out IEmpyrionApi? cached))
                {
                    var factory = _apiImplementations
                        .Where(kvp => kvp.Key == type)
                        .Select(kvp => kvp.Value)
                        .FirstOrDefault();

                    cached = factory != null ? factory(this) : null;
                    _apiCache.Add(type, cached);
                }

                if (cached != null && cached is TEmpyrionApi api)
                    return api;
            }

            return default;
        }
    }
}