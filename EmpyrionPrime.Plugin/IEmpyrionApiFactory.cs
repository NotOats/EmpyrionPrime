namespace EmpyrionPrime.Plugin
{
    /// <summary>
    /// IEmpyrionApi factory used to load various api interfaces at runtime.
    /// Note: This in injected into the constructor of each IEmpyrionPlugin
    /// </summary>
    public interface IEmpyrionApiFactory
    {
        /// <summary>
        /// Creates an api of the specifying type.
        /// Note: This assembly is currently limited to IBasicEmpyrionApi and IExtendedEmpyrionApi.
        ///       Other apis are available in the EmpyrionPrime.ModFramework library.
        /// </summary>
        /// <typeparam name="TEmpyrionApi">The api type</typeparam>
        /// <returns></returns>
        TEmpyrionApi Create<TEmpyrionApi>() where TEmpyrionApi : class, IEmpyrionApi;
    }
}
