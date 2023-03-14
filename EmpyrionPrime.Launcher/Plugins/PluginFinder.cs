using EmpyrionPrime.Plugin;
using System.Reflection;

namespace EmpyrionPrime.Launcher.Plugins;

internal static class PluginFinder
{
    public static IReadOnlyCollection<Type> GetPluginTypes(Assembly assembly)
    {
        if (assembly == null)
            throw new ArgumentNullException(nameof(assembly));

        var result = new List<Type>();

        try
        {
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsAbstract && type.IsAssignableTo(typeof(IEmpyrionPlugin)))
                    result.Add(type);
            }
        }
        catch (ReflectionTypeLoadException)
        {
        }

        return result;
    }

    public static IEnumerable<string> FindAssembliesWithPlugins(string searchPath)
    {
        if (string.IsNullOrEmpty(searchPath))
            throw new ArgumentNullException(nameof(searchPath));

        if (!Directory.Exists(searchPath))
            throw new DirectoryNotFoundException($"searchDirectory not found: {searchPath}");

        var assemblies = Directory.GetFiles(searchPath, "*.dll", new EnumerationOptions { RecurseSubdirectories = true });

        foreach (var assemblyPath in assemblies)
        {
            PluginLoadContext? context = null;

            try
            {
                context = new PluginLoadContext(assemblyPath);

                var assemblyName = new AssemblyName(Path.GetFileNameWithoutExtension(assemblyPath));
                var assembly = context.LoadFromAssemblyName(assemblyName);

                if (GetPluginTypes(assembly).Any())
                    yield return assemblyPath;
            }
            finally
            {
                context?.Unload();
            }
        }

        // force PluginLoadContexts to unload
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}
