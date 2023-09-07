using EmpyrionPrime.ModFramework;
using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.Plugin.Api;
using EmpyrionPrime.RemoteClient.Epm;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.RemoteClient.Console;

internal class ApiManager : IDisposable
{
    private EpmClientAsync? _client;
    private IBasicEmpyrionApi? _empyrionApi;
    private IRequestBroker? _broker;
    private IApiRequests? _apiRequests;

    public IApiRequests ApiRequests => _apiRequests 
        ?? throw new NullReferenceException("ApiRequests is null, possibly disposed?");

    public IRequestBroker Broker => _broker 
        ?? throw new NullReferenceException("RequestBroker is null, possibly disposed?");

    public ApiManager(ILoggerFactory loggerFactory, string address, int port)
    {
        var clientLogger = loggerFactory.CreateLogger("EpmClient");
        _client = new EpmClientAsync(clientLogger, address, port);
        _client.Start();

        _empyrionApi = _client.CreateBasicApi(clientLogger);

        _broker = new RequestBroker(loggerFactory.CreateLogger("Broker"), _empyrionApi);

        _apiRequests = new ApiRequests(loggerFactory.CreateLogger("ApiRequests"), _broker);
    }

    public void Dispose()
    {
        if(_apiRequests != null)
            _apiRequests = null;

        if(_broker is IDisposable broker)
            broker?.Dispose();
        _broker = null;

        if (_empyrionApi is IDisposable api)
            api?.Dispose();
        _empyrionApi = null;

        _client?.Dispose();
        _client = null;
    }
}
