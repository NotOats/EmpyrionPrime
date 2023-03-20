using EmpyrionPrime.Launcher.Extensions;
using EmpyrionPrime.ModFramework.Configuration;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;

namespace EmpyrionPrime.Launcher.Framework.Configuration;

internal class PluginOptionsFactory<TPlugin> : IPluginOptionsFactory<TPlugin> where TPlugin : IEmpyrionPlugin
{
    private readonly Dictionary<string, byte[]> _configFiles;

    private readonly Dictionary<Type, object> _pluginOptionsCache = new();
    private readonly object _pluginOptionsCacheLock = new();
    private readonly IConfiguration _configuration;

    public PluginOptionsFactory(IHostEnvironment env)
    {
        var pluginDirectory = Path.GetDirectoryName(typeof(TPlugin).Assembly.Location)
            ?? throw new Exception("Failed to find plugin directory");

        _configFiles = new[]
            {
                "appsettings.json",
                $"appsettings.{env.EnvironmentName}.json"
            }
            .Select(file => Path.Combine(pluginDirectory, file))
            .ToDictionary(file => file, file => File.Exists(file) ? ComputeHash(file) : new byte[20]);

        var builder = new ConfigurationBuilder()
            .SetBasePath(pluginDirectory);

        foreach(var file in _configFiles)
            builder.AddJsonFile(file.Key, optional: true, reloadOnChange: true);

        _configuration = builder
            .AddEnvironmentVariables(prefix: $"{typeof(TPlugin).Name.ToUpper()}_")
            .Build();

        ChangeToken.OnChange(_configuration.GetReloadToken, HandleChange);
    }

    public IPluginOptions<TOptions> Get<TOptions>() where TOptions : class, new()
    {
        return Get<TOptions>(typeof(TOptions).Name);
    }

    public IPluginOptions<TOptions> Get<TOptions>(string sectionName) where TOptions : class, new()
    {
        lock(_pluginOptionsCacheLock)
        {
            if (_pluginOptionsCache.TryGetValue(typeof(TOptions), out object? cached)
                && cached is IPluginOptions<TOptions> options)
            {
                return options;
            }

            options = new PluginOptions<TOptions>(_configuration, sectionName);
            _pluginOptionsCache.Add(typeof(TOptions), options);

            return options;
        }
    }

    private void HandleChange()
    {
        var changeDetected = false;
        foreach(var file in new Dictionary<string, byte[]>(_configFiles))
        {
            if (!File.Exists(file.Key))
                continue;

            var hash = ComputeHash(file.Key);
            if (file.Value.SequenceEqual(hash))
                continue;

            changeDetected = true;
            _configFiles[file.Key] = hash;
        }

        if (!changeDetected)
            return;

        foreach(var kvp in _pluginOptionsCache
            .AsLocked(_pluginOptionsCacheLock))
        {
            if (kvp.Value is IPluginOptionsChangable changable)
                changable?.HandleSourceChange();
        }
    }

    private static byte[] ComputeHash(string filePath)
    {
        var runCount = 1;

        while(runCount < 4)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using var fs = File.OpenRead(filePath);
                    return System.Security.Cryptography.SHA1
                        .Create().ComputeHash(fs);
                }
                else
                {
                    throw new FileNotFoundException("Could not compute hash, fine not found", filePath);
                }
            }
            catch (IOException ex)
            {
                if (runCount == 3 || ex.HResult != -2147024864)
                {
                    throw;
                }
                else
                {
                    Thread.Sleep(TimeSpan.FromSeconds(Math.Pow(2, runCount)));
                    runCount++;
                }
            }
        }

        return new byte[20];
    }
}
