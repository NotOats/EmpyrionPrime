namespace EmpyrionPrime.Plugin
{
    public interface IEmpyrionGameApi<TPlugin> : IEmpyrionGameApi where TPlugin : IEmpyrionPlugin
    {
    }
}
