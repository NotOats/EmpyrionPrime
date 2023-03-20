using EmpyrionPrime.Launcher;
using EmpyrionPrime.Launcher.Empyrion;
using EmpyrionPrime.Launcher.Plugins;
using EmpyrionPrime.Plugin;
using EmpyrionPrime.RemoteClient;
using EmpyrionPrime.RemoteClient.Epm;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Enrichers.ShortTypeName;

var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? Environments.Development;
var configuration = new ConfigurationBuilder()
    .SetBasePath(Environment.CurrentDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables(prefix: "DOTNET_")
    .AddCommandLine(args)
    .Build();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(config =>
    {
        config.Sources.Clear();
        config.AddConfiguration(configuration);
    })
    .UseSerilog((context, services, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.With<ShortTypeNameEnricher>();
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<EmpyrionSettings>(context.Configuration.GetSection("Empyrion"));
        services.Configure<PluginsSettings>(context.Configuration.GetSection("Plugins"));

        services.AddSingleton<IRemoteEmpyrion>(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<EpmClient>>();
            var settings = provider.GetRequiredService<IOptions<EmpyrionSettings>>().Value;

            var client = new EpmClient(logger, settings.EpmAddress, settings.EpmPort, settings.EpmClientId);
            client.Start();

            return client;
        });

        services.AddSingleton(typeof(IEmpyrionApiFactory<>), typeof(EmpyrionApiFactory<>));
        services.AddSingleton<IPluginManager, PluginManager>();
        services.AddHostedService<ModInterfaceBroker>();
    });

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.With<ShortTypeNameEnricher>()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Launcher...");

    var host = builder.Build();
    host.Run();

    return 0;
}
catch(Exception ex)
{
    Log.Fatal(ex, "Launcher terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}