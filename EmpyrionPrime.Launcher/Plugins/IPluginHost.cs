﻿using EmpyrionPrime.Mod;

namespace EmpyrionPrime.Launcher.Plugins;

internal interface IPluginHost : IDisposable
{
    public string AssemblyName { get; }
    public IReadOnlyCollection<WeakReference<IEmpyrionPlugin>> Plugins { get; }
    
}
