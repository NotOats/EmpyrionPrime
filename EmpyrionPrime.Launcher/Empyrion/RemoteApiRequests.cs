using EmpyrionPrime.ModFramework;
using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.ModFramework.Extensions;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class RemoteApiRequests<TPlugin> : ApiRequests where TPlugin : IEmpyrionPlugin
{
    public RemoteApiRequests(ILoggerFactory loggerFactory, IRequestBroker requestBroker)
    : base(loggerFactory.CreateLogger<TPlugin, ApiRequests>(), requestBroker)
    {
    }
}
