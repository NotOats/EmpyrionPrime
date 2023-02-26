using System.Reflection;
using System.Runtime.Loader;

namespace EmpyrionPrime.Launcher.Plugins;

internal class PluginLoadContext : AssemblyLoadContext
{
    private readonly AssemblyDependencyResolver _resolver;

    public PluginLoadContext(string pluginPath) : base(true)
    {
        if(string.IsNullOrEmpty(pluginPath))
            throw new ArgumentNullException(nameof(pluginPath));

        if (!File.Exists(pluginPath))
            throw new FileNotFoundException("Invalid plugin path", pluginPath);

        _resolver = new AssemblyDependencyResolver(pluginPath);
    }

    protected override Assembly? Load(AssemblyName assemblyName)
    {
        var assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);

        if (assemblyPath == null)
            return null;

        //return LoadFromAssemblyPath(assemblyPath);
        using var stream = File.Open(assemblyPath, FileMode.Open, FileAccess.Read);

        return LoadFromStream(stream);
    }

    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        var libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);

        if(libraryPath == null)
            return IntPtr.Zero;

        return LoadUnmanagedDllFromPath(libraryPath);
    }
}
