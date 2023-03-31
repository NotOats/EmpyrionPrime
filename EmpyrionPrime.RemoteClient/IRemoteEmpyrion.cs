using Eleon.Modding;
using EmpyrionPrime.Plugin;
using Microsoft.Extensions.Logging;
using System;

namespace EmpyrionPrime.RemoteClient
{
    public interface IRemoteEmpyrion
    {
        /// <summary>
        /// Called when the IRemoteEmpryion instance connects to the game.
        /// </summary>
        event Action OnConnected;

        /// <summary>
        /// Called when the IRemoteEmpyrion instances disconnects from the game.
        /// </summary>
        event Action OnDisconnected;

        /// <summary>
        /// Called for each event received from the game.
        /// </summary>
        event Action<GameEvent> GameEventHandler;

        /// <summary>
        /// Sends a request to the game server
        /// </summary>
        /// <param name="id">The request's id</param>
        /// <param name="sequenceNumber">The request's sequence number, this is returned in the follow up response.</param>
        /// <param name="payload">The request's optional payload data</param>
        void SendRequest(CmdId id, ushort sequenceNumber, object payload);

        /// <summary>
        /// Creates an instance of IBasicEmpyrionApi for the IRemoteEmpyrion client
        /// </summary>
        /// <param name="logger">The logger the interface will use</param>
        /// <returns></returns>
        IBasicEmpyrionApi CreateBasicApi(ILogger logger);

        /// <summary>
        /// Creates an instance of IExtendedEmpyrionApi for the IRemoteEmpyrion client
        /// </summary>
        /// <param name="logger">The logger the interface will use</param>
        /// <returns></returns>
        IExtendedEmpyrionApi CreateExtendedApi(ILogger logger);
    }
}
