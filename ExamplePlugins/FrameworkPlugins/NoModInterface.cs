using Eleon.Modding;
using EmpyrionPrime.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkPlugins;

public class NoModInterface : IEmpyrionPlugin
{
    public string Name => "NoModInterface";
    public string Author => "NotOats";
    public Version Version => new(1, 0);
    public ModInterface? ModInterface { get; } = null;

    public NoModInterface()
    {

    }
}
