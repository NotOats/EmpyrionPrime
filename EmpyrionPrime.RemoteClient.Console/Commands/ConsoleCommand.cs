using Eleon.Modding;
using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.RemoteClient.Console.Settings;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace EmpyrionPrime.RemoteClient.Console.Commands;

internal class ConsoleCommandSettings : GlobalSettings
{
    [CommandArgument(0, "<command>")]
    [Description("The console command to run on the server")]
    public string? Command { get; set; }

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(Command))
            return ValidationResult.Error("Invalid command");

        return base.Validate();
    }
}

internal class ConsoleCommand : AsyncCommand<ConsoleCommandSettings>
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger _logger;

    public ConsoleCommand()
    {
        _loggerFactory = Logging.Factory;
        _logger = _loggerFactory.CreateLogger("ConsoleCommand");

        if (!Logging.QuietMode)
            AnsiConsole.Write(new FigletText("RemoteClient Console").Centered());
    }

    public override async Task<int> ExecuteAsync(CommandContext context, ConsoleCommandSettings settings)
    {
        settings.OverrideFromConfigFile();
        if (!settings.Validate().Successful)
            return (int)ErrorCodes.CommandSettings;

        ApiManager? manager = null;

        try
        {
            manager = new ApiManager(_loggerFactory, settings.EpmAddress!, settings.EpmPort);
            if (!await manager.Connect(false))
                return (int)ErrorCodes.ServerConnection;

            _logger.LogInformation("Sending command: \"{command}\"", settings.Command);

            var payload = new PString(settings.Command);
            await manager.ApiRequests.ConsoleCommand(payload);

            _logger.LogInformation("Finished");
        }
        catch (AggregateException ae)
        {
            if (ae.InnerExceptions.FirstOrDefault(x => x is EmpyrionRequestException) is EmpyrionRequestException requestException)
            {
                // Simplified output in case quite mode
                // TODO: Sort this out better when logging settings are a thing
                System.Console.WriteLine($"Error: {requestException.RequestError}");

                return (int)ErrorCodes.RequestError;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to run command");
            return (int)ErrorCodes.Unknown;
        }
        finally
        {
            manager?.Dispose();
        }

        return (int)ErrorCodes.None;
    }
}
