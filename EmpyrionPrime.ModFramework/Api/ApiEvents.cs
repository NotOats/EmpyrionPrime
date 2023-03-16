using Eleon.Modding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpyrionPrime.ModFramework
{
    public delegate Task AsyncGameEventHandler();
    public delegate Task AsyncGameEventHandler<TEventArgs>(TEventArgs e);

    public partial class ApiEvents
    {
        private readonly IDictionary<CmdId, Delegate> _eventHandlers = new Dictionary<CmdId, Delegate>();
        private readonly object _eventHandlersLock = new object();

        private readonly ILogger _logger;

        internal protected ApiEvents(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        internal protected void HandleGameEvent(CmdId commandId, ushort sequenceNumber, object data)
        {
            Delegate handler = null;

            lock (_eventHandlersLock)
            {
                if (!_eventHandlers.TryGetValue(commandId, out handler) || handler == null)
                    return;
            }

#if DEBUG
            _logger.LogDebug("Handling Event: {commandId}, {sequenceNumber}, {dataType}",
                commandId, sequenceNumber, data?.GetType());
#endif

            Task.Factory.StartNew(async state =>
            {
                try
                {
                    var tuple = (Tuple<Delegate, object>)state;
                    var result = tuple.Item1.DynamicInvoke(tuple.Item2);

                    if (result is Task task)
                        await task;
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Error when executing event handler");
                }

            }, Tuple.Create(handler, data));
        }

        private void AddHandler(CmdId commandId, Delegate handler)
        {
            if(handler == null)
                throw new ArgumentNullException(nameof(handler));

#if DEBUG
            _logger.LogDebug("Added {handler} to {commandId}", handler, commandId);
#endif

            lock (_eventHandlersLock)
            {
                if (_eventHandlers.ContainsKey(commandId))
                    _eventHandlers[commandId] = Delegate.Combine(_eventHandlers[commandId], handler);
                else
                    _eventHandlers[commandId] = handler;
            }
        }

        private void RemoveHandler(CmdId commandId, Delegate handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

#if DEBUG
            _logger.LogDebug("ARemoving {handler} from {commandId}", handler, commandId);
#endif

            lock (_eventHandlersLock)
            {
                if (_eventHandlers.ContainsKey(commandId))
                    _eventHandlers[commandId] = Delegate.Remove(_eventHandlers[commandId], handler);
            }
        }
    }
}
