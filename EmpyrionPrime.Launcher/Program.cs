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
        // TODO: Use SimpleInjector recommended configuration register -> Change all IOptions<T> to just T
        services.Configure<EmpyrionSettings>(context.Configuration.GetSection("Empyrion"));
        services.Configure<PluginsSettings>(context.Configuration.GetSection("Plugins"));

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

    AddInstancesToContainer(container, configuration);

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

static void AddInstancesToContainer(Container container, IConfigurationRoot configuration)
{
    // TODO: Change ModInterfaceBroker to not need this
    // Register IServiceProvider for use in ModInterfaceBroker
    container.RegisterInstance<IServiceProvider>(container);

    // Directly register settings which SimpleInjector prefers to IOptions<T>
    RegisterSettings<EmpyrionSettings>(container, configuration, "Empyrion");
    RegisterSettings<PluginsSettings>(container, configuration, "Plugins");

    // Register empyrion interfaces
    container.RegisterSingleton<IRemoteEmpyrion>(() =>
    {
        var logger = container.GetInstance<ILogger<EpmClient>>();
        var settings = container.GetInstance<EmpyrionSettings>();

        var client = new EpmClient(logger, settings.EpmAddress, settings.EpmPort, settings.EpmClientId);
        client.Start();

        return client;
    });

    container.RegisterSingleton<IPluginManager, PluginManager>();

#if DEBUG
    container.Verify();
#endif
}

static void RegisterSettings<T>(Container container, IConfigurationRoot configuration, string? section = null) where T : class
{
    section ??= typeof(T).Name;

    var instance = configuration.GetSection(section).Get<T>() 
        ?? throw new Exception($"Failed to create {typeof(T).Name} from '{section}'");

    // TODO: Configuration validation?

    container.RegisterInstance<T>(instance);
}