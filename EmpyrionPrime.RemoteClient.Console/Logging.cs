using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.RemoteClient.Console;

internal static class Logging
{
    public static bool QuietMode { get; } = Environment.GetCommandLineArgs().Any(arg => arg == "-q" || arg == "--quite");

    public static ILoggerFactory Factory { get; } = LoggerFactory.Create(builder =>
    {
        builder.ClearProviders();
        builder.SetMinimumLevel(QuietMode ? LogLevel.Critical : LogLevel.Information);

        builder.AddSimpleConsole(options =>
        {
            options.IncludeScopes = false;
            options.SingleLine = true;
        });
    });
}
