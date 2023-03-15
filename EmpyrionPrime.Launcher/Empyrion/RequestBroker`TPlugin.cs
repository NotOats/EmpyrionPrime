using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class RequestBroker<TPlugin> : RequestBroker, IRequestBroker<TPlugin>, IDisposable where TPlugin : IEmpyrionPlugin
{
    private readonly IEmpyrionGameApi _empyrionGameApi;

    public RequestBroker(ILogger<RequestBroker> logger, IEmpyrionGameApi<TPlugin> empyrionGameApi)
        : base(logger, empyrionGameApi.ModGameAPI)
    {
        _empyrionGameApi = empyrionGameApi;
        _empyrionGameApi.GameEvent += HandleGameEvent;
    }

    public void Dispose()
    {
        _empyrionGameApi.GameEvent -= HandleGameEvent;
    }
}
