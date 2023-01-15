using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UniversityDbContext>(optionsAction: options => options.UseSqlServer(
            connectionString: configuration.GetConnectionString(name: "PlanBeeUniversityDatabase"), 
            sqlServerOptionsAction: builder => builder.MigrationsAssembly("Start")));

        services.AddDatabaseRepositories();

        return services;
    }

    private static IServiceCollection AddDatabaseRepositories(this IServiceCollection services)
    {
        services.AddTransient<IBaseUserReadRepository, BaseUserRepository>();
        services.AddTransient<IBaseUserWriteRepository, BaseUserRepository>();

        return services;
    }
}