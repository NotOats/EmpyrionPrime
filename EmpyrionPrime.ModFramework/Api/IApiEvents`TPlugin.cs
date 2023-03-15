using EmpyrionPrime.Plugin;

namespace EmpyrionPrime.ModFramework.Api
{
    /// <summary>
    /// Interface to load ApiEvents via Dependency Injection
    /// </summary>
    /// <typeparam name="TPlugin">IEmpyrionPlugin the instance of ApiEvents is bound to</typeparam>
    public interface IApiEvents<TPlugin> : IApiEvents where TPlugin : IEmpyrionPlugin
    {
    }
}
