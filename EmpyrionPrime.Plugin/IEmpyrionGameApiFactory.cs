using System;

namespace EmpyrionPrime.Plugin
{
    public interface IEmpyrionGameApiFactory
    {
        IEmpyrionGameApi CreateGameApi(Type pluginType);

        IEmpyrionGameApi CreateGameApi<T>() where T : IEmpyrionPlugin;
    }
}
