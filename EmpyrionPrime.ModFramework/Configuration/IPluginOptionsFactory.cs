using EmpyrionPrime.Plugin;

namespace EmpyrionPrime.ModFramework.Configuration
{
    public interface IPluginOptionsFactory<TPlugin> where TPlugin : IEmpyrionPlugin
    {
        IPluginOptions<TOptions> Get<TOptions>() where TOptions : class, new();
        IPluginOptions<TOptions> Get<TOptions>(string sectionName) where TOptions : class, new();
    }
}
