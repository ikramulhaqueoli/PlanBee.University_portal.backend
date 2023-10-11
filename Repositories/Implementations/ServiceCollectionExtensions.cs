using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.DesignationDomain;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;
using PlanBee.University_portal.backend.Domain.Entities.UniTemplateDomain;
using PlanBee.University_portal.backend.Domain.Entities.UserVerificationDomain;
using PlanBee.University_portal.backend.Domain.Entities.WorkplaceDomain;
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

        services.AddTransient<IMongoReadRepository, MongoRepository>();
        services.AddTransient<IMongoWriteRepository, MongoRepository>();

        services.AddTransient<IBaseUserReadRepository, BaseUserRepository>();
        services.AddTransient<IBaseUserWriteRepository, BaseUserRepository>();

        services.AddTransient<IEmployeeReadRepository, EmployeeRepository>();
        services.AddTransient<IEmployeeWriteRepository, EmployeeRepository>();

        services.AddTransient<IDesignationReadRepository, DesignationRepository>();
        services.AddTransient<IDesignationWriteRepository, DesignationRepository>();

        services.AddTransient<IWorkplaceReadRepository, WorkplaceRepository>();
        services.AddTransient<IWorkplaceWriteRepository, WorkplaceRepository>();

        services.AddTransient<IRegistrationRequestReadRepository, RegistrationRequestRepository>();
        services.AddTransient<IRegistrationRequestWriteRepository, RegistrationRequestRepository>();

        services.AddTransient<IUniTemplateReadRepository, UniTemplateRepository>();

        services.AddTransient<IUserVerificationReadRepository, UserVerificationRepository>();
        services.AddTransient<IUserVerificationWriteRepository, UserVerificationRepository>();
    }
}