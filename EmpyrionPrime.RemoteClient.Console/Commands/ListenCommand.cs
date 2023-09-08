using Eleon.Modding;
using EmpyrionPrime.RemoteClient.Console.Settings;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmpyrionPrime.RemoteClient.Console.Commands;

internal class ListenSettings : GlobalSettings
{
    [CommandOption("-f|--event-filter")]
    [Description("Only show the specified events. This can be an event id number or event name found in Eleon.Modding.CmdId (ex: Event_Playfield_Loaded)")]
    public string? EventFilter { get; set; }

    [CommandOption("-o|--output-format")]
    [Description("Format the events will be outputted as. Supported: text, json")]
    [DefaultValue("text")]
    public string? OutputFormat { get; set; }

    public override ValidationResult Validate()
    {
        if(!string.IsNullOrWhiteSpace(EventFilter) && ParseEventFilter() == null)
            return ValidationResult.Error("Invalid event filter");

        var formats = new[] { "text", "json" };
        if (OutputFormat == null || !formats.Contains(OutputFormat.ToLower()))
            return ValidationResult.Error("Invalid output format");

        return base.Validate();
    }

    public int? ParseEventFilter()
    {
        if (string.IsNullOrWhiteSpace(EventFilter))
            return null;

        if (Enum.TryParse(EventFilter, true, out CmdId cmdId))
            return (int)cmdId;

        if(int.TryParse(EventFilter, out int cmdInt))
            return cmdInt;

        return null;
    }
}

internal class ListenCommand : AsyncCommand<ListenSettings>
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger _logger;
    private readonly TaskCompletionSource _tcs = new();

    public ListenCommand()
    {
        _loggerFactory = Logging.Factory;
        _logger = _loggerFactory.CreateLogger("RequestCommand");

        if (!Logging.QuietMode)
            AnsiConsole.Write(new FigletText("RemoteClient Console").Centered());

        // Hook cancellation key to exit listen loop
        System.Console.CancelKeyPress += (s, e) =>
        {
            _tcs.SetResult();

            e.Cancel = true;
        };
    }

    public override async Task<int> ExecuteAsync(CommandContext context, ListenSettings settings)
    {
        var eventFilter = settings.ParseEventFilter();

        if (!Logging.QuietMode)
            AnsiConsole.WriteLine($"Listening for {(eventFilter != null ? settings.EventFilter : "all events")}. Press Ctrl+C to shut down.");

        ApiManager? manager = null;

        try
        {
            manager = new ApiManager(_loggerFactory, settings.EpmAddress!, settings.EpmPort);
            manager.EmpyrionApi.GameEvent += (cmdId, seqNum, data) =>
            {
                if (eventFilter != null && (int)cmdId != eventFilter)
                    return;

                var gameEvent = new WrappedGameEvent { CmdId = cmdId, SeqNum = seqNum, Data = data };
                var output = gameEvent.Format(settings.OutputFormat!);

                System.Console.WriteLine(output);
            };

            // Wait for cancellation request
            await _tcs.Task.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to run command");
            return 1;
        }
        finally
        {
            manager?.Dispose();
        }

        return 0;
    }

    private readonly struct WrappedGameEvent
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CmdId CmdId { get; init; }

        public ushort SeqNum { get; init; }

        public object Data { get; init; }

        public string Format(string format)
        {
            var serialize = (object? obj) =>
            {
                return JsonSerializer.Serialize(obj, new JsonSerializerOptions { IncludeFields = true });
            };

            var output = format switch
            {
                "json" => serialize(this),
                _ => $"Event: {CmdId}, Seq: {SeqNum}, Data: {serialize(Data)}",
            };

            return output;
        }
    }
}
