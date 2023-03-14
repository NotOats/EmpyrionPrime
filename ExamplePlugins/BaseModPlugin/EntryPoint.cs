using Eleon.Modding;
using EmpyrionPrime.Mod;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModPlugin;

public class EntryPoint : IEmpyrionPlugin
{
    public string Name => "BaseMod Plugin";
    public string Author => "NotOats";
    public Version Version => new(1, 0);

    public ModInterface ModInterface { get; }

    public EntryPoint(ILoggerFactory loggerFactory)
    {
        ModInterface = new ExampleModInterface(loggerFactory);
    }
}
