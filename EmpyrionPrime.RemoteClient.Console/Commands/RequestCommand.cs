using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.ModFramework.Extensions;
using EmpyrionPrime.RemoteClient.Console.Settings;
using EmpyrionPrime.Schema.ModInterface;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Text.Json;

namespace EmpyrionPrime.RemoteClient.Console.Commands;

internal class RequestSettings : GlobalSettings
{
}

internal class RequestWithPayloadSettings : GlobalSettings
{
    [CommandArgument(0, "<payload>")]
    [Description("JSON object containing the request payload. Ex: '{\"id\": 123456, \"credits\": 10.0}'")]
    public string? Payload { get; set; }

    public override ValidationResult Validate()
    {
        // TODO: Validate this is JSON & fits with Request type
        if (string.IsNullOrWhiteSpace(Payload))
            return ValidationResult.Error("Invalid payload");

        return base.Validate();
    }
}

internal class RequestCommand<TSettings> : AsyncCommand<TSettings> where TSettings : GlobalSettings
{
    private static readonly TimeSpan DefaultTimeout = new(0, 0, 0, 10);

    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger _logger;

    public RequestCommand()
    {
        _loggerFactory = Logging.Factory;
        _logger = _loggerFactory.CreateLogger("RequestCommand");

        if (!Logging.QuietMode)
            AnsiConsole.Write(new FigletText("RemoteClient Console").Centered());
    }

    public override async Task<int> ExecuteAsync(CommandContext context, TSettings settings)
    {
        settings.OverrideFromConfigFile();
        if (!settings.Validate().Successful)
            return (int)ErrorCodes.CommandSettings;

        if (context.Data is not ApiRequest request)
            throw new InvalidOperationException("Command has no associated data");

        if (!ReadPayload(request, settings, out object? payload))
            return (int)ErrorCodes.CommandPayload;

        ApiManager? manager = null;

        try
        {
            manager = new ApiManager(_loggerFactory, settings.EpmAddress!, settings.EpmPort);
            if (!await manager.Connect(false))
                return (int)ErrorCodes.ServerConnection;

            var response = await manager.Broker.SendGameRequest(request.CommandId, payload, request.ResponseId == null)
                .TimeoutAfter((int)DefaultTimeout.TotalMilliseconds);

            // No response, we're done.
            if (response == null && request.ResponseType == null)
                return 0;

            // Read & display response
            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions {IncludeFields = true});
            System.Console.WriteLine(json);
        }
        catch(AggregateException ae)
        {
            if (ae.InnerExceptions.FirstOrDefault(x => x is EmpyrionRequestException) is EmpyrionRequestException requestException)
            {
                // Simplified output in case quite mode
                // TODO: Sort this out better when logging settings are a thing
                System.Console.WriteLine($"Error: {requestException.RequestError}");

                return (int)ErrorCodes.RequestError;
            }
        }
        catch(Exception ex)
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

    public override ValidationResult Validate(CommandContext context, TSettings settings)
    {
        var valid = settings.Validate();
        if (!valid.Successful)
            return valid;

        return base.Validate(context, settings);
    }

    private bool ReadPayload(ApiRequest request, TSettings settings, out object? payload)
    {
        payload = null;

        var payloadJson = settings is RequestWithPayloadSettings payloadSettings ? payloadSettings.Payload : null;
        if (payloadJson == null || request.ArgumentType == null)
            return true;

        try
        {
            payload = JsonSerializer.Deserialize(payloadJson, request.ArgumentType, new JsonSerializerOptions { IncludeFields = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to run command");
            return false;
        }

        return true;
    }
}
