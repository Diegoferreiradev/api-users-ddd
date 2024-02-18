using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Crosscutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependeciesRepository(IServiceCollection servicesCollection)
        {
            servicesCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            servicesCollection.AddScoped(typeof(IUserRepository), typeof(UserImplementation));

            servicesCollection.AddDbContext<MyContext>(
           options => options.UseSqlServer("Server=DESKTOP-UHLS283;Database=dbAPI;Trusted_Connection=True;"));
        }
    }
}
