using EmpyrionPrime.ModFramework;
using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class ApiEvents<TPlugin> : ApiEvents, IApiEvents<TPlugin>, IDisposable where TPlugin : IEmpyrionPlugin
{
    private readonly IEmpyrionGameApi<TPlugin> _empyrionGameApi;

    public ApiEvents(ILogger<ApiEvents> logger, IEmpyrionGameApi<TPlugin> empyrionGameApi)
        : base(logger)
    {
        _empyrionGameApi = empyrionGameApi;
        _empyrionGameApi.GameEvent += HandleGameEvent;
    }

    public void Dispose()
    {
        _empyrionGameApi.GameEvent -= HandleGameEvent;
    }
}
