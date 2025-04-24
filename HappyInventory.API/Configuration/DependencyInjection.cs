using HappyInventory.Data.Repositories;
using HappyInventory.Models.Models.Identity;

namespace HappyInventory.API.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<User>, Repository<User>>();

            return services;
        }
    }
}
