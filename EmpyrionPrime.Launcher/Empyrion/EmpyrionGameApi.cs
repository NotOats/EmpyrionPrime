using Eleon.Modding;
using EmpyrionPrime.Mod;
using EmpyrionPrime.RemoteClient;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class EmpyrionGameApi : IEmpyrionGameApi, IDisposable
{
    private readonly ILogger _logger;
    private readonly IRemoteEmpyrion _remoteEmpyrion;
    private readonly RemoteModGameApi _remoteModGameApi;

    public ModGameAPI ModGameAPI
    {
        get { return _remoteModGameApi; }
    }

    public EmpyrionGameApi(ILogger logger, IRemoteEmpyrion remoteEmpyrion)
    {
        _logger = logger;
        _remoteEmpyrion = remoteEmpyrion;

        _remoteModGameApi = new RemoteModGameApi(_remoteEmpyrion);
        _remoteModGameApi.ConsoleWriteHandler += HandleConsoleWrite;
        _remoteModGameApi.GetTickTimeHandler += HandleGetTickTime;
    }

    public void Dispose()
    {
        _remoteModGameApi.ConsoleWriteHandler -= HandleConsoleWrite;
        _remoteModGameApi.GetTickTimeHandler -= HandleGetTickTime;
    }

    private void HandleConsoleWrite(string text)
    {
        _logger.LogInformation("{text}", text);
    }

    private ulong HandleGetTickTime()
    {
        return (ulong)Environment.TickCount;
    }
}
