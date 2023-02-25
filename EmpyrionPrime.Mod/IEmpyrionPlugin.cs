using Eleon.Modding;
using System;

namespace EmpyrionPrime.Mod
{
    public interface IEmpyrionPlugin
    {
        string Name { get; }
        Version Version { get; }
        ModInterface ModInterface { get; }
    }
}
