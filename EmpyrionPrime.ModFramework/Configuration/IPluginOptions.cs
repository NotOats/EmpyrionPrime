using System;

namespace EmpyrionPrime.ModFramework.Configuration
{
    /// <summary>
    /// This represents a section of the plugin's configuration system.
    /// It is strongly typed to TOptions
    /// </summary>
    /// <typeparam name="TOptions">The options class</typeparam>
    public interface IPluginOptions<TOptions> where TOptions : class
    {
        /// <summary>
        /// The current value of TOptions
        /// </summary>
        TOptions Value { get; }

        /// <summary>
        /// Triggered when a change is detected in the plugin's configuration file
        /// </summary>
        event Action OptionsChanged;
    }
}
