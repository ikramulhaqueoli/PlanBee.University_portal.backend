using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations;

public static class ServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UniversityDbContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("PlanBeeUniversityDatabase"),
            builder => builder.MigrationsAssembly("Start")));

        services.AddDatabaseRepositories();
    }

    private static void AddDatabaseRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseUserReadRepository, BaseUserRepository>();
        services.AddScoped<IBaseUserWriteRepository, BaseUserRepository>();

        services.AddScoped<IRegistrationRequestWriteRepository, RegistrationRequestRepository>();
    }
}