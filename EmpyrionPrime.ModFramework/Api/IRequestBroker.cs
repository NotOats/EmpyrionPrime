using Eleon.Modding;
using EmpyrionPrime.Plugin;
using System.Threading.Tasks;

namespace EmpyrionPrime.ModFramework.Api
{
    /// <summary>
    /// Asynchronously handles sending and receiving events from an Empyrion server.
    /// </summary>
    public interface IRequestBroker : IEmpyrionApi
    {
        /// <summary>
        /// Send a game request with attached data and returns a TaskCompletionSource that will complete
        /// when the corresponding event is returned from the server.
        /// </summary>
        /// <param name="eventId">The event Id</param>
        /// <param name="data">The event data</param>
        /// <returns>Task that will complete with the corresponding event from the server</returns>
        Task<object> SendGameRequest(CmdId eventId, object data, bool noResponse = false);
    }
}
