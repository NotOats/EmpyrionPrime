using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Net;

namespace EmpyrionPrime.RemoteClient.Console.Settings;

internal class GlobalSettings : CommandSettings
{
    [CommandOption("-a|--epm-address")]
    [DefaultValue("127.0.0.1")]
    public string? EpmAddress { get; set; }

    [CommandOption("-p|--epm-port")]
    [DefaultValue("12345")]
    public int EpmPort { get; set; }

    [CommandOption("-q||--quiet")]
    [DefaultValue(false)]
    [Description("Runs the program in quiet mode (useful for automation/scripting)")]
    public bool QuietMode { get; set; }

    public override ValidationResult Validate()
    {
        if (!IPAddress.TryParse(EpmAddress, out _))
            return ValidationResult.Error("Invalid EpmAddress");

        if (EpmPort < 1 || EpmPort > 65535)
            return ValidationResult.Error("Invalid EpmPort");

        return base.Validate();
    }

    public void OverrideFromConfigFile()
    {
        // TODO: Add config file support
    }
}
