using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.ModFramework.Extensions;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class RequestBroker<TPlugin> : RequestBroker, IRequestBroker<TPlugin>, IDisposable where TPlugin : IEmpyrionPlugin
{
    private readonly IEmpyrionGameApi _empyrionGameApi;

    public RequestBroker(ILoggerFactory loggerFactory, IEmpyrionGameApi<TPlugin> empyrionGameApi)
        : base(loggerFactory.CreateLogger<TPlugin, RequestBroker>(), empyrionGameApi.ModGameAPI)
    {
        _empyrionGameApi = empyrionGameApi;
        _empyrionGameApi.GameEvent += HandleGameEvent;
    }

    public void Dispose()
    {
        _empyrionGameApi.GameEvent -= HandleGameEvent;
    }
}
