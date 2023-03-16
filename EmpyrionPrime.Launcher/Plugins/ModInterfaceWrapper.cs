using Eleon.Modding;
using EmpyrionPrime.Plugin;
using System.Reflection;

namespace EmpyrionPrime.Launcher.Plugins;

internal class ModInterfaceWrapper<TModInterface> : IEmpyrionPlugin where TModInterface : ModInterface
{
    public string Name { get; }
    public string Author { get; }
    public Version Version { get; }
    public ModInterface ModInterface { get; }

    public ModInterfaceWrapper()
    {
        // Try AssemblyTitle first, then Assembly file name, finally TModInterface name
        Name = GetAttribute<AssemblyTitleAttribute>()?.Title 
            ?? typeof(TModInterface).Assembly.GetName().Name 
            ?? typeof(TModInterface).Name;

        // Not sure where else to check for this, possibly parse <Mod>_Info.yaml?
        Author = GetAttribute<AssemblyCompanyAttribute>()?.Company ?? "Unknown Author";

        // Pull the version string from Assembly, then try AssemblyFile, and finally no version
        Version = new Version(GetAttribute<AssemblyVersionAttribute>()?.Version
            ?? GetAttribute<AssemblyFileVersionAttribute>()?.Version
            ?? "0.0");

        ModInterface = Activator.CreateInstance<TModInterface>();
    }

    private static TAttribute? GetAttribute<TAttribute>() where TAttribute : Attribute
    {
        var attributes = typeof(TModInterface).Assembly.GetCustomAttributes(typeof(TAttribute), false);

        if (attributes == null || attributes.Length == 0)
            return null;

        return attributes.OfType<TAttribute>().FirstOrDefault();
    }
}