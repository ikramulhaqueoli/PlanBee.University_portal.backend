using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Models;

namespace PlanBee.University_portal.backend.Repositories.Implementations;

public static class ServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services, AppConfig appConfig)
    {
        services.AddDatabaseRepositories(appConfig);
    }


    private static void AddDatabaseRepositories(this IServiceCollection services, AppConfig appConfig)
    {
        var mongoClient = new MongoClient(appConfig.MongoDatabase.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(appConfig.MongoDatabase.DatabaseName);

        services.AddSingleton(typeof(IMongoDatabase), mongoDatabase);
        services.AddTransient<IMongoDbCollectionProvider, MongoDbCollectionProvider>();

        services.AddTransient<IBaseUserReadRepository, BaseUserRepository>();
        services.AddTransient<IBaseUserWriteRepository, BaseUserRepository>();

        services.AddTransient<IRegistrationRequestWriteRepository, RegistrationRequestRepository>();
    }
}