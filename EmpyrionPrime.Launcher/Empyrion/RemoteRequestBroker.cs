using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.ModFramework.Extensions;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class RemoteRequestBroker<TPlugin> : RequestBroker , IDisposable where TPlugin : IEmpyrionPlugin
{
    private readonly IBasicEmpyrionApi _empyrionApi;

    public RemoteRequestBroker(ILoggerFactory loggerFactory, IBasicEmpyrionApi empyrionApi)
        : base(loggerFactory.CreateLogger<TPlugin, RequestBroker>(), empyrionApi)
    {
        _empyrionApi = empyrionApi;
        _empyrionApi.GameEvent += HandleGameEvent;
    }

    public void Dispose()
    {
        _empyrionApi.GameEvent -= HandleGameEvent;
    }
}
