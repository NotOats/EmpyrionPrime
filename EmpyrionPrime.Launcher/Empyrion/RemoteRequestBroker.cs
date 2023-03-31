using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.ModFramework.Extensions;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class RemoteRequestBroker<TPlugin> : RequestBroker , IDisposable where TPlugin : IEmpyrionPlugin
{
    private readonly IBasicEmpyrionApi _empyrionApi;

    internal RemoteRequestBroker(ILoggerFactory loggerFactory, IEmpyrionApiFactory<TPlugin> apiFactory)
        : base(loggerFactory.CreateLogger<TPlugin, RequestBroker>(), apiFactory.Create<IBasicEmpyrionApi>())
    {
        _empyrionApi = apiFactory.Create<IBasicEmpyrionApi>();
        _empyrionApi.GameEvent += HandleGameEvent;
    }

    public void Dispose()
    {
        _empyrionApi.GameEvent -= HandleGameEvent;
    }
}
