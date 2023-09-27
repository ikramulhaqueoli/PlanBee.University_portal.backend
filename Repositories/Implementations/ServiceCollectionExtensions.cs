using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations;

public static class ServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseRepositories(configuration);
    }


    private static void AddDatabaseRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("SoftBeeDatabase");
        var mongoClient = new MongoClient(section["ConnectionString"]?.ToString());
        var mongoDatabase = mongoClient.GetDatabase(section["DatabaseName"]?.ToString());

        services.AddSingleton(typeof(IMongoDatabase), mongoDatabase);
        services.AddTransient<IMongoDbCollectionProvider, MongoDbCollectionProvider>();

        services.AddTransient<IBaseUserReadRepository, BaseUserRepository>();
        services.AddTransient<IBaseUserWriteRepository, BaseUserRepository>();

        services.AddTransient<IRegistrationRequestWriteRepository, RegistrationRequestRepository>();
    }
}