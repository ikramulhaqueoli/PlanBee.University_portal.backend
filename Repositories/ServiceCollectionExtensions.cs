using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PlanBee.University_portal.backend.Repositories;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UniversityDbContext>(optionsAction: options => options.UseSqlServer(
            connectionString: configuration.GetConnectionString(name: "PlanBeeUniversityDatabase"), 
            sqlServerOptionsAction: builder => builder.MigrationsAssembly("Start")));

        return services;
    }
}