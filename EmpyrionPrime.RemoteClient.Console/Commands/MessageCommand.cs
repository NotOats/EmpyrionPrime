using EmpyrionPrime.RemoteClient.Console.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Runtime;
using EmpyrionPrime.ModFramework.Api;
using Eleon;

namespace EmpyrionPrime.RemoteClient.Console.Commands;

internal class MessageCommandSettings : GlobalSettings
{
    [CommandArgument(0, "<message>")]
    [Description("The message to send to chat")]
    public string? Message { get; set; }

    [CommandOption("--sender-type")]
    [Description("Message sender type, this is a value from Eleon.Modding.SenderType")]
    [DefaultValue(SenderType.ServerInfo)]
    public SenderType SenderType { get; set; }

    [CommandOption("--sender-name")]
    [Description("Optional sender name override")]
    public string? SenderName { get; set; }

    [CommandOption("--channel")]
    [Description("The channel to send to, this is a value from Eleon.Modding.MsgChannel")]
    [DefaultValue(MsgChannel.Global)]
    public MsgChannel Channel { get; set; }

    [CommandOption("--recipient-id")]
    [Description("Either an entity or faction id depending on the Channel type")]
    public int? RecipientId { get; set; }

    [CommandOption("--post-send-delay")]
    [Description("Due to issues with EPM there is no response when sending a chat message. The program will delay this many seconds after the api call to ensure it's sent.")]
    [DefaultValue(1)]
    public int PostSendDelay { get; set; }

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(Message))
            return ValidationResult.Error("Invalid message");

        if (Channel == MsgChannel.SinglePlayer && RecipientId == null)
            return ValidationResult.Error("Recipient Id must be set when the Channel type is SinglePlayer");

        if (Channel == MsgChannel.Faction && RecipientId == null)
            return ValidationResult.Error("Recipient Id must be set when the Channel type is Faction");

        return base.Validate();
    }
}

internal class MessageCommand : AsyncCommand<MessageCommandSettings>
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger _logger;

    public MessageCommand()
    {
        _loggerFactory = Logging.Factory;
        _logger = _loggerFactory.CreateLogger("MessageCommand");

        if (!Logging.QuietMode)
            AnsiConsole.Write(new FigletText("RemoteClient Console").Centered());
    }

    public override async Task<int> ExecuteAsync(CommandContext context, MessageCommandSettings settings)
    {
        settings.OverrideFromConfigFile();
        if(!settings.Validate().Successful)
            return (int)ErrorCodes.CommandSettings;

        ApiManager? manager = null;

        try
        {
            manager = new ApiManager(_loggerFactory, settings.EpmAddress!, settings.EpmPort);
            if (!await manager.Connect(false))
                return (int)ErrorCodes.ServerConnection;

            var api = manager.ExtendedEmpyrionApi;
            if (api == null)
                return (int)ErrorCodes.ApiError;

            var msg = CreateMessage(settings);
            api.SendChatMessage(msg);

            await Task.Delay(settings.PostSendDelay * 1000);

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

    public override ValidationResult Validate(CommandContext context, MessageCommandSettings settings)
    {
        var valid = settings.Validate();
        if (!valid.Successful)
            return valid;

        return base.Validate(context, settings);
    }

    private static MessageData CreateMessage(MessageCommandSettings settings)
    {
        var msg = new MessageData
        {
            Text = settings.Message,
            SenderType = settings.SenderType,
            Channel = settings.Channel
        };

        if(!string.IsNullOrWhiteSpace(settings.SenderName))
            msg.SenderNameOverride = settings.SenderName;

        if (settings.Channel == MsgChannel.SinglePlayer)
            msg.RecipientEntityId = settings.RecipientId!.Value;

        if (settings.Channel == MsgChannel.Faction)
            msg.RecipientFaction = new FactionData
            {
                Group = FactionGroup.Faction,
                Id = settings.RecipientId!.Value
            };

        return msg;
    }
}
