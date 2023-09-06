using EmpyrionPrime.RemoteClient.Console;
using EmpyrionPrime.RemoteClient.Console.Commands;
using Spectre.Console;
using Spectre.Console.Cli;

if(!Logging.QuietMode)
    AnsiConsole.Write(new FigletText("RemoteClient Console").Centered());

var app = new CommandApp();
app.Configure(config =>
{
    config.CaseSensitivity(CaseSensitivity.None);
    config.ValidateExamples();

    config.AddCommand<ConsoleCommand>("run")
        .WithAlias("console-command")
        .WithDescription("Runs a console command on the server");
});

await app.RunAsync(args);