using EmpyrionPrime.Launcher;
using EmpyrionPrime.Launcher.Plugins;
using EmpyrionPrime.RemoteClient;
using EmpyrionPrime.RemoteClient.Epm;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Enrichers.ShortTypeName;
using SimpleInjector;

var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? Environments.Development;
var configuration = new ConfigurationBuilder()
    .SetBasePath(Environment.CurrentDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables(prefix: "DOTNET_")
    .AddCommandLine(args)
    .Build();

var container = new Container();

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
        services.AddSimpleInjector(container, options =>
        {
            options.AddLogging();

            options.AddHostedService<ModInterfaceBroker>();
        });
    });

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.With<ShortTypeNameEnricher>()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Launcher...");

    var host = builder.Build()
        .UseSimpleInjector(container);

    RegisterSettings(container, configuration);
    RegisterInterfaces(container, configuration);

#if DEBUG
    container.Verify();
#endif

    host.Run();

    return 0;
}
catch(Exception ex)
{
    Log.Fatal(ex, "Launcher terminated unexpectedly");
#if DEBUG
    throw;
#else
    return 1;
#endif
}
finally
{
    Log.CloseAndFlush();
}

static void RegisterInterfaces(Container container, IConfigurationRoot configuration)
{
    // Load service provider from container
    container.RegisterInstance<IServiceProvider>(container);

    // Temporarily create EmpyrionSettings to figure out which RemoteClient to register
    var launcherSettings = LoadSettings<LauncherSettings>(configuration, "Launcher");
    Func<IRemoteEmpyrion> loader = launcherSettings.RemoteClient switch
    {
        "EpmClientAsync" => () =>
            {
                var logger = container.GetInstance<ILogger<EpmClient>>();
                var configuration = container.GetInstance<IConfigurationRoot>();
                var settings = LoadSettings<EpmClientSettings>(configuration, "EpmClient");

                var instance = new EpmClientAsync(logger, settings);
                instance.Start();

                return instance;
            },
        _ => throw new Exception($"Unknown RemoteClient type of '{launcherSettings.RemoteClient}'"),
    };
    
    // Continue with registration
    container.RegisterSingleton(loader);
    container.RegisterSingleton<IPluginManager, PluginManager>();
}

static void RegisterSettings(Container container, IConfigurationRoot configuration)
{
    container.RegisterInstance(configuration);
    container.RegisterInstance(LoadSettings<LauncherSettings>(configuration, "Launcher"));
    container.RegisterInstance(LoadSettings<PluginsSettings>(configuration, "Plugins"));
}

static T LoadSettings<T>(IConfigurationRoot configuration, string? section = null) where T : class
{
    section ??= typeof(T).Name;

    var instance = configuration.GetSection(section).Get<T>()
        ?? throw new Exception($"Failed to create {typeof(T).Name} from '{section}'");

    if (instance is ISettingsValidator validator)
        validator.Validate();

    return instance;
}