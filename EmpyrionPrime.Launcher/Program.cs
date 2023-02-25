using EmpyrionPrime.Launcher;
using EmpyrionPrime.Launcher.Plugins;
using EmpyrionPrime.RemoteClient;
using EmpyrionPrime.RemoteClient.Epm;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


var host = Host.CreateApplicationBuilder();

// Configuration
host.Configuration.Sources.Clear();
host.Configuration.AddJsonFile("appsettings.json");
host.Configuration.AddEnvironmentVariables();
host.Configuration.AddCommandLine(args);

// Logging
host.Logging.ClearProviders();
host.Logging.AddConfiguration(host.Configuration);
host.Logging.AddConsole();

// Specific Settings
host.Services.Configure<EmpyrionSettings>(host.Configuration.GetSection("Empyrion"));

// Services
host.Services.AddRemoteEmpyrion();
host.Services.RegisterPluginHosts();
host.Services.AddHostedService<ModInterfaceManager>();

// Run the app
var app = host.Build();
app.Run();

internal static class ServiceCollectionExtensions
{
    public static void AddRemoteEmpyrion(this IServiceCollection services)
    {
        services.AddSingleton<IRemoteEmpyrion>(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<EpmClient>>();
            var settings = provider.GetRequiredService<EmpyrionSettings>();

            var clientId = settings.EpmClientId;
            if (clientId == -1)
                clientId = Environment.ProcessId;

            var client = new EpmClient(logger, settings.EpmAddress, settings.EpmPort, clientId);
            client.Start();

            return client;
        });
    }

    public static void RegisterPluginHosts(this IServiceCollection services)
    {
        var pluginPath = Path.Join(Environment.CurrentDirectory, "Plugins");
        if (!Directory.Exists(pluginPath))
            return;

        var assemblyPaths = PluginFinder.FindAssembliesWithPlugins(pluginPath);
        foreach(var assemblyPath in assemblyPaths)
        {
            services.AddSingleton<IPluginHost>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<PluginHost>>();
                return new PluginHost(provider, logger, assemblyPath);
            });
        }
    }
}
