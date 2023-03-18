namespace EmpyrionPrime.Plugin
{
    public interface IEmpyrionApiFactory<TPlugin> where TPlugin: IEmpyrionPlugin
    {
        TEmpyrionApi Create<TEmpyrionApi>() where TEmpyrionApi : class, IEmpyrionApi;
    }
}
