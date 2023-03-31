namespace EmpyrionPrime.Plugin
{
    public interface IEmpyrionApiFactory
    {
        TEmpyrionApi Create<TEmpyrionApi>() where TEmpyrionApi : class, IEmpyrionApi;
    }
}
