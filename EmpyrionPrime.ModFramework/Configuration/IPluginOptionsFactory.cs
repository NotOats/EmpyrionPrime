namespace EmpyrionPrime.ModFramework.Configuration
{
    /// <summary>
    /// Factory for reading options from the plugin's configuration system
    /// </summary>
    public interface IPluginOptionsFactory
    {
        /// <summary>
        /// Reads an options object from the nameof(TOptions) section of the plugin's configuration system.
        /// </summary>
        /// <typeparam name="TOptions">The options object type</typeparam>
        /// <returns></returns>
        IPluginOptions<TOptions> Get<TOptions>() where TOptions : class, new();

        /// <summary>
        /// Reads an option object from the specified section of the plugin's configuration system
        /// </summary>
        /// <typeparam name="TOptions">The options object type</typeparam>
        /// <param name="sectionName">The section name to read from</param>
        /// <returns></returns>
        IPluginOptions<TOptions> Get<TOptions>(string sectionName) where TOptions : class, new();
    }
}
