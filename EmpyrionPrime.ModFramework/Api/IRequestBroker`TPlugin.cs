using EmpyrionPrime.Plugin;

namespace EmpyrionPrime.ModFramework.Api
{
    /// <summary>
    /// Interface to load RequestBroker via Dependency Injection
    /// </summary>
    /// <typeparam name="TPlugin">IEmpyrionPlugin the instance of RequestBroker is bound to</typeparam>
    public interface IRequestBroker<TPlugin> : IRequestBroker where TPlugin : IEmpyrionPlugin
    {
    }
}
