using Eleon.Modding;
using EmpyrionPrime.Plugin;
using System.Threading.Tasks;

namespace EmpyrionPrime.ModFramework.Api
{
    public interface IRequestBroker : IEmpyrionApi
    {
        Task<object> SendGameRequest(CmdId eventId, object data);
    }
}
