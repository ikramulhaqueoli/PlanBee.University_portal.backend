using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Repositories.Implementations;

namespace PlanBee.University_portal.backend.Repositories;

public static class ServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseRepositories(configuration);
    }


    private static void AddDatabaseRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("SoftBeeDatabase");
        var mongoClient = new MongoClient(section["ConnectionString"]);
        var mongoDatabase = mongoClient.GetDatabase(section["DatabaseName"]);

        services.AddSingleton(typeof(IMongoDatabase), mongoDatabase);
        services.AddScoped<IMongoDbCollectionProvider, MongoDbCollectionProvider>();

        services.AddScoped<IBaseUserReadRepository, BaseUserRepository>();
        services.AddScoped<IBaseUserWriteRepository, BaseUserRepository>();

        services.AddScoped<IRegistrationRequestWriteRepository, RegistrationRequestRepository>();
    }
}