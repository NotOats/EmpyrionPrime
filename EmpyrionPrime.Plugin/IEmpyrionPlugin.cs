using Eleon.Modding;
using System;

namespace EmpyrionPrime.Plugin
{
    public interface IEmpyrionPlugin
    {
        string Name { get; }
        string Author { get; }
        Version Version { get; }
        ModInterface ModInterface { get; }
    }
}
