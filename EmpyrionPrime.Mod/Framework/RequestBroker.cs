using Eleon.Modding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace EmpyrionPrime.Mod.Framework
{
    public class RequestBroker
    {
        private readonly ILogger _logger;
        private readonly ModGameAPI _gameApi;
        private readonly ConcurrentDictionary<ushort, TaskCompletionSource<object>> _pendingRequests = 
            new ConcurrentDictionary<ushort, TaskCompletionSource<object>>();

        private readonly static object _sequenceLock = new object();
        private readonly static int _sequenceStartNumber = 4096;
        private static int _nextSequenceNumber = new Random().Next(_sequenceStartNumber, ushort.MaxValue);

        internal RequestBroker(ILogger<RequestBroker> logger, ModGameAPI gameApi)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _gameApi = gameApi ?? throw new ArgumentNullException(nameof(gameApi));
        }

        public async Task<object> SendGameRequest(CmdId eventId, object data)
        {
            var tcs = new TaskCompletionSource<object>();

            ushort sequenceNumber;

            do
            {
                lock(_sequenceLock)
                {
                    if(_nextSequenceNumber == ushort.MaxValue)
                        _nextSequenceNumber = _sequenceStartNumber;

                    sequenceNumber = (ushort)_nextSequenceNumber++;
                }
            } while (!_pendingRequests.TryAdd(sequenceNumber, tcs));

            _logger.LogDebug("TCS created for {eventId}, seqNr: {sequenceNumber}, data: {dataType}",
                eventId, sequenceNumber, data?.GetType());

            _gameApi.Game_Request(eventId, sequenceNumber, data);

            return await tcs.Task;
        }

        public void HandleGameEvent(CmdId eventId, ushort sequenceNumber, object data)
        {
            if (!_pendingRequests.TryRemove(sequenceNumber, out TaskCompletionSource<object> tcs))
                return;

            if (eventId == CmdId.Event_Error && data is ErrorInfo errorInfo)
            {
                var errorMessage = errorInfo.errorType.ToString();
                tcs.TrySetException(new Exception(errorMessage));

                _logger.LogError("Error with request {sequenceNumber}: {errorMessage}", sequenceNumber, errorMessage);

                return;
            }

            if (tcs.TrySetResult(data))
                _logger.LogDebug("Request {sequenceNumber} completed", sequenceNumber);
            else
                _logger.LogError("Request {sequenceNumber} failed to set result", sequenceNumber);
        }
    }
}
