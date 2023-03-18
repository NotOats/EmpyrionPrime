using EmpyrionPrime.ModFramework;
using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.ModFramework.Extensions;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class RemoteApiRequests<TPlugin> : ApiRequests where TPlugin : IEmpyrionPlugin
{
    public RemoteApiRequests(ILoggerFactory loggerFactory, IEmpyrionApiFactory<TPlugin> apiFactory)
    : base(loggerFactory.CreateLogger<TPlugin, ApiRequests>(), apiFactory.Create<IRequestBroker>())
    {
    }
}
