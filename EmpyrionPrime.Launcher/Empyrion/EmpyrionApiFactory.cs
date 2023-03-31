using EmpyrionPrime.Plugin;
using Microsoft.Extensions.DependencyInjection;

namespace EmpyrionPrime.Launcher.Empyrion
{
    internal class EmpyrionApiFactory : IEmpyrionApiFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public EmpyrionApiFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TEmpyrionApi? Create<TEmpyrionApi>() where TEmpyrionApi : class, IEmpyrionApi
        {
            return _serviceProvider.GetService<TEmpyrionApi>();
        }
    }
}
