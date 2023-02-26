using EmpyrionPrime.Launcher;
using EmpyrionPrime.Launcher.Empyrion;
using EmpyrionPrime.Launcher.Plugins;
using EmpyrionPrime.Mod;
using EmpyrionPrime.RemoteClient;
using EmpyrionPrime.RemoteClient.Epm;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
host.Services.Configure<PluginsSettings>(host.Configuration.GetSection("Plugins"));

// Services
host.Services.AddRemoteEmpyrion();
host.Services.AddSingleton<IEmpyrionGameApi, EmpyrionGameApi>();
host.Services.AddSingleton<IPluginManager, PluginManager>();
host.Services.AddHostedService<ModInterfaceBroker>();

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
            var settings = provider.GetRequiredService<IOptions<EmpyrionSettings>>();

            var clientId = settings.Value.EpmClientId;
            if (clientId == -1)
                clientId = Environment.ProcessId;

            var client = new EpmClient(logger, settings.Value.EpmAddress, settings.Value.EpmPort, clientId);
            client.Start();

            return client;
        });
    }
}
