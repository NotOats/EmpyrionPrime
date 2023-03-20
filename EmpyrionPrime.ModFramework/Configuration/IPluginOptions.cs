using System;

namespace EmpyrionPrime.ModFramework.Configuration
{
    public interface IPluginOptions<TOptions> where TOptions : class
    {
        TOptions Value { get; }

        event Action OptionsChanged;
    }
}
