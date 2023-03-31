using EmpyrionPrime.Plugin;

namespace EmpyrionPrime.ModFramework.Configuration
{
    public interface IPluginOptionsFactory
    {
        IPluginOptions<TOptions> Get<TOptions>() where TOptions : class, new();
        IPluginOptions<TOptions> Get<TOptions>(string sectionName) where TOptions : class, new();
    }
}
