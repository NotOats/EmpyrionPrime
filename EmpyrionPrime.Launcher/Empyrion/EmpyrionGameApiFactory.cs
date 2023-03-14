using EmpyrionPrime.Plugin;
using EmpyrionPrime.RemoteClient;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class EmpyrionGameApiFactory : IEmpyrionGameApiFactory
{
    private readonly ConcurrentDictionary<Type, IEmpyrionGameApi> _gameApiCache = new();

    private readonly ILoggerFactory _loggerFactory;
    private readonly IRemoteEmpyrion _remoteEmpyrion;

    public EmpyrionGameApiFactory(ILoggerFactory loggerFactory, IRemoteEmpyrion remoteEmpyrion)
    {
        _loggerFactory = loggerFactory;
        _remoteEmpyrion = remoteEmpyrion;
    }

    public IEmpyrionGameApi CreateGameApi(Type pluginType)
    {
        return _gameApiCache.GetOrAdd(pluginType, type =>
        {
            var pluginLogger = _loggerFactory.CreateLogger(type);
            return new RemoteEmpyrionGameApi(pluginLogger, _remoteEmpyrion);
        });
    }

    public IEmpyrionGameApi CreateGameApi<T>() where T : IEmpyrionPlugin
    {
        return CreateGameApi(typeof(T));
    }
}
