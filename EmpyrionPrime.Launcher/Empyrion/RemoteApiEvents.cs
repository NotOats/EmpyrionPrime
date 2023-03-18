using EmpyrionPrime.ModFramework;
using EmpyrionPrime.ModFramework.Extensions;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class RemoteApiEvents<TPlugin> : ApiEvents, IDisposable where TPlugin : IEmpyrionPlugin
{
    private readonly IBasicEmpyrionApi _empyrionApi;

    internal RemoteApiEvents(ILoggerFactory loggerFactory, IEmpyrionApiFactory<TPlugin> apiFactory)
        : base(loggerFactory.CreateLogger<TPlugin, ApiEvents>())
    {
        _empyrionApi = apiFactory.Create<IBasicEmpyrionApi>();
        _empyrionApi.GameEvent += HandleGameEvent;
    }

    public void Dispose()
    {
        _empyrionApi.GameEvent -= HandleGameEvent;
    }
}
