using EmpyrionPrime.Plugin;

namespace EmpyrionPrime.ModFramework.Api
{
    /// <summary>
    /// Interface to load ApiRequests via Dependency Injection
    /// </summary>
    /// <typeparam name="TPlugin">IEmpyrionPlugin the instance of ApiRequests is bound to</typeparam>
    public interface IApiRequests<TPlugin> : IApiRequests where TPlugin : IEmpyrionPlugin
    {
    }
}
