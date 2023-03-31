using EmpyrionPrime.ModFramework;
using EmpyrionPrime.ModFramework.Extensions;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class RemoteApiEvents<TPlugin> : ApiEvents, IDisposable where TPlugin : IEmpyrionPlugin
{
    private readonly IBasicEmpyrionApi _empyrionApi;

    public RemoteApiEvents(ILoggerFactory loggerFactory, IBasicEmpyrionApi empyrionApi)
        : base(loggerFactory.CreateLogger<TPlugin, ApiEvents>())
    {
        _empyrionApi = empyrionApi;
        _empyrionApi.GameEvent += HandleGameEvent;
    }

    public void Dispose()
    {
        _empyrionApi.GameEvent -= HandleGameEvent;
    }
}
