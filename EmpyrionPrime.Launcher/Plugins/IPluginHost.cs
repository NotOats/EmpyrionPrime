using EmpyrionPrime.Mod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpyrionPrime.Launcher.Plugins
{
    internal interface IPluginHost : IDisposable
    {
        public string AssemblyName { get; }
        public IReadOnlyCollection<WeakReference<IEmpyrionPlugin>> Plugins { get; }
        
    }
}
