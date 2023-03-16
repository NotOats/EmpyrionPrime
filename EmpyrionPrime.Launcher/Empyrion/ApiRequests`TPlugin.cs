using EmpyrionPrime.ModFramework;
using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.ModFramework.Extensions;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class ApiRequests<TPlugin> : ApiRequests, IApiRequests<TPlugin> where TPlugin : IEmpyrionPlugin
{
    public ApiRequests(ILoggerFactory loggerFactory, IRequestBroker<TPlugin> requestBroker)
        : base(loggerFactory.CreateLogger<TPlugin, ApiRequests>(), requestBroker)
    {
    }
}
