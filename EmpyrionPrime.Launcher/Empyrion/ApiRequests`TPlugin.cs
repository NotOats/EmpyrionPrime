using EmpyrionPrime.ModFramework;
using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class ApiRequests<TPlugin> : ApiRequests, IApiRequests<TPlugin> where TPlugin : IEmpyrionPlugin
{
    public ApiRequests(ILogger<ApiRequests> logger, IRequestBroker<TPlugin> requestBroker)
        : base(logger, requestBroker)
    {
    }
}
