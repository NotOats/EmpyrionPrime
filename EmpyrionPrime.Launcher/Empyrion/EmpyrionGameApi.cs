using Eleon.Modding;
using EmpyrionPrime.Mod;
using EmpyrionPrime.RemoteClient;
using EmpyrionPrime.RemoteClient.Api;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpyrionPrime.Launcher.Empyrion;

internal class EmpyrionGameApi : IEmpyrionGameApi, IDisposable
{
    private readonly ILogger<EmpyrionGameApi> _logger;
    private readonly IRemoteEmpyrion _remoteEmpyrion;
    private readonly RemoteModGameApi _remoteModGameApi;

    public ModGameAPI ModGameAPI
    {
        get { return _remoteModGameApi; }
    }

    public EmpyrionGameApi(ILogger<EmpyrionGameApi> logger, IRemoteEmpyrion remoteEmpyrion)
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
        _logger.LogInformation("ModGameAPI.Console_Write: {text}", text);
    }

    private ulong HandleGetTickTime()
    {
        return (ulong)Environment.TickCount;
    }
}
