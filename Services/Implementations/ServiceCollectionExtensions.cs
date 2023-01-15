using Microsoft.Extensions.DependencyInjection;

namespace PlanBee.University_portal.backend.Services.Implementations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IUserSignupService, UserSignupService>();

        return services;
    }
}