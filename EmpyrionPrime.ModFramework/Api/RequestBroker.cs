﻿using Eleon.Modding;
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
        /// </summary>
        /// <param name="eventId">The event Id</param>
        /// <param name="data">The event data</param>
        /// <returns>Task that will complete with the corresponding event from the server</returns>
        /// <exception cref="ObjectDisposedException">Thrown is the class is disposed</exception>
        public async Task<object> SendGameRequest(CmdId eventId, object data)
        {
            if (_disposeCount > 0) throw new ObjectDisposedException(GetType().Name);

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

            if(_basicEmpyrionApi != null)
                _basicEmpyrionApi.SendEvent(eventId, sequenceNumber, data);
            else
                _modGameApi.Game_Request(eventId, sequenceNumber, data);

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
    }
}
