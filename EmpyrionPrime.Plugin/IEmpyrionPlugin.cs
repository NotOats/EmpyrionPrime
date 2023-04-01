using Eleon.Modding;
using System;

namespace EmpyrionPrime.Plugin
{
    /// <summary>
    /// Interface for an Empyrion Plugin loaded via EmpyrionPrime.Launcher
    /// </summary>
    public interface IEmpyrionPlugin
    {
        /// <summary>
        /// The name of the plugin
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The author of the plugin
        /// </summary>
        string Author { get; }

        /// <summary>
        /// The plugin version
        /// </summary>
        Version Version { get; }

        /// <summary>
        /// Optional ModInterface for supporting existing Empyrion mods
        /// </summary>
        ModInterface ModInterface { get; }
    }
}
