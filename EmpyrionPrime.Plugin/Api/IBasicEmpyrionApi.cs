using Eleon.Modding;

namespace EmpyrionPrime.Plugin.Api
{
    /// <summary>
    /// A game event handler
    /// </summary>
    /// <param name="commandId">The event's Id</param>
    /// <param name="sequenceNumber">The event's sequence number</param>
    /// <param name="data">The event's data, see commandId for the specific object type</param>
    public delegate void GameEventHandler(CmdId commandId, ushort sequenceNumber, object data);

    /// <summary>
    /// Basic Empyrion api used to replace ModInterface
    /// </summary>
    public interface IBasicEmpyrionApi : IEmpyrionApi
    {
        /// <summary>
        /// Event triggers on each ModInterface.Game_Event call
        /// </summary>
        event GameEventHandler GameEvent;

        /// <summary>
        /// Used to send an event to the game server via ModGameApi.Game_Request
        /// </summary>
        /// <param name="id">The event Id</param>
        /// <param name="sequenceNumber">The sequence number, this will be used in the return GameEventHandler</param>
        /// <param name="data">The event data</param>
        void SendEvent(CmdId id, ushort sequenceNumber, object data);
    }
}
