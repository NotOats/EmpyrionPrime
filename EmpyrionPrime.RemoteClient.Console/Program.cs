using EmpyrionPrime.RemoteClient.Console.Commands;
using EmpyrionPrime.Schema.ModInterface;
using Spectre.Console.Cli;

var app = new CommandApp();
app.Configure(config =>
{
    config.CaseSensitivity(CaseSensitivity.None);
    config.ValidateExamples();

    config.AddCommand<ConsoleCommand>("run")
        .WithAlias("console-command")
        .WithDescription("Runs a console command on the server")
        .WithExample("run", "-q", "\"say 'Some message from the server here.'\"");

    config.AddCommand<ListenCommand>("listen")
        .WithDescription("Listens for events from the server")
        .WithExample("listen", "-f", "Event_Dedi_Stats");

    config.AddBranch<CommandSettings>("request", parent =>
    {
        parent.SetDescription("Runs a request on the server");
        parent.AddExample(new[] { "request", "PlayfieldList", "-q" });
        parent.AddExample(new[] { "request", "PlayfieldStats", "-q", @"""{\""pstr\"":\""Haven Sector\""}""" });

        foreach(var request in ApiSchema.ApiRequests)
        {
            var name = request.CommandId.ToString().Replace("Request_", "").Replace("_", "");
            var alias = request.CommandId.ToString().Replace("Request_", "").ToLower();
            var argType = request.ArgumentType != null ? request.ArgumentType.FullName : "None";
            var retType = request.ResponseType != null ? request.ResponseType.FullName : "None";

            if(request.ArgumentType != null)
            {
                parent.AddCommand<RequestCommand<RequestWithPayloadSettings>>(name)
                    .WithAlias(alias)
                    .WithDescription($"Runs {request.CommandId} on the server.\n     Argument: {argType}\n     Response: {retType}")
                    .WithData(request);
            } 
            else
            {
                parent.AddCommand<RequestCommand<RequestSettings>>(name)
                    .WithAlias(alias)
                    .WithDescription($"Runs {request.CommandId} on the server.\n     Response: {retType}")
                    .WithData(request);
            }
        }
    });
});

return await app.RunAsync(args);