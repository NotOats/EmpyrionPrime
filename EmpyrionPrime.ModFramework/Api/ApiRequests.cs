using Eleon.Modding;
using EmpyrionPrime.ModFramework.Api;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace EmpyrionPrime.ModFramework
{
    public partial class ApiRequests
    {
        private readonly static TimeSpan DefaultTimeout = new TimeSpan(0, 0, 0, 10);

        private readonly ILogger _logger;
        private readonly IRequestBroker _requestBroker;

        internal protected ApiRequests(ILogger logger, IRequestBroker requestBroker)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _requestBroker = requestBroker ?? throw new ArgumentNullException(nameof(requestBroker));
        }

        private class DebugLog : IDisposable
        {
            private readonly ILogger _logger;
            private readonly CmdId _commandId;
            private readonly Stopwatch _stopwatch;

            public DebugLog(ILogger logger, CmdId commandId)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _commandId = commandId;

                _logger.LogDebug("Starting new request for {commandId}", _commandId);
                _stopwatch = Stopwatch.StartNew();
            }

            public void Dispose()
            {
                _stopwatch.Stop();

                _logger.LogDebug("Finished request for {commandId} in {elapsed:n}ms", _commandId, _stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
