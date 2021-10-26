using Infrastructure.Persistance;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<FunctionResultStorageLogger>();
            services.AddSingleton<FunctionResultStorage>();

            return services;
        }
    }
}