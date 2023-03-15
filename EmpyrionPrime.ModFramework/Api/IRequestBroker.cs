using Eleon.Modding;
using System.Threading.Tasks;

namespace EmpyrionPrime.ModFramework.Api
{
    public interface IRequestBroker
    {
        Task<object> SendGameRequest(CmdId eventId, object data);
    }
}
