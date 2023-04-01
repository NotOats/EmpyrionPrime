using Eleon.Modding;
using EmpyrionPrime.Plugin.Api;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace EmpyrionPrime.ModFramework.Api
{
    /// <summary>
    /// Asynchronously handles sending and receiving events from an Empyrion server.
    /// </summary>
    public class RequestBroker : IRequestBroker, IDisposable
    {
        private readonly ILogger _logger;
        private readonly ModGameAPI _modGameApi;
        private readonly IBasicEmpyrionApi _basicEmpyrionApi;

        private readonly ConcurrentDictionary<ushort, TaskCompletionSource<object>> _pendingRequests =
            new ConcurrentDictionary<ushort, TaskCompletionSource<object>>();

        private readonly static object _sequenceLock = new object();
        private readonly static int _sequenceStartNumber = 4096;
        private static int _nextSequenceNumber = new Random().Next(_sequenceStartNumber, ushort.MaxValue);

        private int _disposeCount = 0;

        internal RequestBroker(ILogger logger, ModGameAPI modGameApi)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _modGameApi = modGameApi ?? throw new ArgumentNullException(nameof(modGameApi));
            _basicEmpyrionApi = null;
        }

        /// <summary>
        /// Creates a RequestBroker that automatically handles reading events from the specified IBasicEmpyrionApi
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="basicEmpyrionApi">The empyrion api to use</param>
        /// <exception cref="ArgumentNullException">Thrown if logger or basicEmpyrionApi is null</exception>
        public RequestBroker(ILogger logger, IBasicEmpyrionApi basicEmpyrionApi)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _basicEmpyrionApi = basicEmpyrionApi ?? throw new ArgumentNullException(nameof(basicEmpyrionApi));
            _modGameApi = null;

            _basicEmpyrionApi.GameEvent += HandleGameEvent;
        }

        public void Dispose()
        {
            if (Interlocked.Increment(ref _disposeCount) != 1)
                return;

            if(_basicEmpyrionApi != null)
                _basicEmpyrionApi.GameEvent -= HandleGameEvent;
        }

        /// <summary>
        /// Send a game request with attached data and returns a TaskCompletionSource that will complete
        /// when the corresponding event is returned from the server.
        /// 
        /// Note: If the request has no response it will still reserve a sequenceNumber and TaskCompletionSource.
        ///       These will be disposed of in a new task after a short period of time.
        /// </summary>
        /// <param name="eventId">The event Id</param>
        /// <param name="data">The event data</param>
        /// <param name="noResponse">Optional flag if the request has no response.</param>
        /// <returns>Task that will complete with the corresponding event from the server</returns>
        /// <exception cref="ObjectDisposedException">Thrown is the class is disposed</exception>
        public async Task<object> SendGameRequest(CmdId eventId, object data, bool noResponse = false)
        {
            if (_disposeCount > 0) throw new ObjectDisposedException(GetType().Name);

            // Reserve TCS and SequenceNumber
            var tcs = new TaskCompletionSource<object>();
            ushort sequenceNumber;

            do
            {
                lock (_sequenceLock)
                {
                    if (_nextSequenceNumber == ushort.MaxValue)
                        _nextSequenceNumber = _sequenceStartNumber;

                    sequenceNumber = (ushort)_nextSequenceNumber++;
                }
            } while (!_pendingRequests.TryAdd(sequenceNumber, tcs));

            _logger.LogDebug("TCS created for {eventId}, seqNr: {sequenceNumber}, data: {dataType}",
                eventId, sequenceNumber, data?.GetType());


            // Short delay and then clean up pending request
            if (noResponse)
            {
                _ = Task.Run(async () =>
                {
                    // TODO: Configurable delay
                    await Task.Delay(100);

                    if (_pendingRequests.TryRemove(sequenceNumber, out _))
                        _logger.LogDebug("Manually handling response for {eventId}, seqNr: {sequenceNumber}", eventId, sequenceNumber);
                    else
                        _logger.LogError("Failed to find response for {eventId}, seqNr: {sequenceNumber}", eventId, sequenceNumber);

                    tcs.TrySetResult(null);
                });
            }

            // Make the request
            MakeRequest(eventId, sequenceNumber, data);

            return await tcs.Task;
        }

        internal void HandleGameEvent(CmdId eventId, ushort sequenceNumber, object data)
        {
            // TODO: Validate sequence number against expected response. This will require storing the source eventId
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

        private void MakeRequest(CmdId eventId, ushort sequenceNumber, object data)
        {
            if (_basicEmpyrionApi != null)
                _basicEmpyrionApi.SendEvent(eventId, sequenceNumber, data);
            else if (_modGameApi != null)
                _modGameApi.Game_Request(eventId, sequenceNumber, data);
            else
                throw new Exception("RequestBroker has no api implementation");
        }
    }
}
