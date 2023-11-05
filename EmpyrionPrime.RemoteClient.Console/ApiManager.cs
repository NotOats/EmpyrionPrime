using EmpyrionPrime.ModFramework;
using EmpyrionPrime.ModFramework.Api;
using EmpyrionPrime.Plugin.Api;
using EmpyrionPrime.RemoteClient.Epm;
using Microsoft.Extensions.Logging;

namespace EmpyrionPrime.RemoteClient.Console;

internal class ApiManager : IDisposable
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger _logger;

    private EpmClientAsync? _client;
    private IBasicEmpyrionApi? _empyrionApi;
    private IExtendedEmpyrionApi? _extendedEmpyrionApi;
    private IRequestBroker? _broker;
    private IApiRequests? _apiRequests;

    public IApiRequests ApiRequests => _apiRequests 
        ?? throw new NullReferenceException("ApiRequests is null, possibly disposed?");

    public IRequestBroker Broker => _broker 
        ?? throw new NullReferenceException("RequestBroker is null, possibly disposed?");

    public IBasicEmpyrionApi EmpyrionApi => _empyrionApi 
        ?? throw new NullReferenceException("BasicEmpyrionApi is null, possibly disposed?");

    public IExtendedEmpyrionApi? ExtendedEmpyrionApi => _extendedEmpyrionApi;

    public ApiManager(ILoggerFactory loggerFactory, string address, int port)
    {
        _loggerFactory = loggerFactory;
        _logger = loggerFactory.CreateLogger<ApiManager>();

        var clientLogger = _loggerFactory.CreateLogger("EpmClient");
        _client = new EpmClientAsync(clientLogger, address, port);
    }

    public async Task<bool> Connect(bool retryOnError = true)
    {
        if (_client == null)
            return false;

        var connected = false;
        try
        {
            await _client.Connect(retryOnError);
            connected = true;
            _client.Start();
        }
        catch (Exception ex)
        {
            if (!connected)
                _logger.LogError(ex, "Failed to connect to server");
            else
                _logger.LogError(ex, "Failed to start reader/writer tasks");

            return false;
        }

        var clientLogger = _loggerFactory.CreateLogger("EpmClient");
        _empyrionApi = _client.CreateBasicApi(clientLogger);
        _extendedEmpyrionApi = _client.CreateExtendedApi(clientLogger);

        _broker = new RequestBroker(_loggerFactory.CreateLogger("Broker"), _empyrionApi);
        _apiRequests = new ApiRequests(_loggerFactory.CreateLogger("ApiRequests"), _broker);

        return true;
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
