using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Crosscutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependeciesService(IServiceCollection servicesCollection)
        {
            servicesCollection.AddTransient<IUserService, UserService>();
            servicesCollection.AddTransient<ILoginService, LoginService>();
        }
    }
}
