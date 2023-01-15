using Microsoft.Extensions.DependencyInjection;
using PlanBee.University_portal.backend.CommandHandlers.Implementations;
using PlanBee.University_portal.backend.CommandHandlers.Implementations.Validators;

namespace PlanBee.University_portal.backend.CommandHandlers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.AddHandlers();
        services.AddValidators();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        var types = typeof(AbstractCommandValidator<>).Assembly.GetTypes()
            .Where(type => type.IsAbstract is false).ToList();

        var commands = types.Where(type => type.Name.EndsWith("Command")).ToList();
        var handlers = types.Where(type => type.Name.EndsWith("CommandHandler")).ToList();
        
        foreach (var command in commands)
        {
            services.AddTransient(
                serviceType: typeof(ICommandValidator<>).MakeGenericType(command),
                implementationType: handlers.First(h => h.Name.StartsWith(command.Name)));
        }

        return services;
    }
    
    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        var types = typeof(AbstractCommandValidator<>).Assembly.GetTypes()
            .Where(type => type.IsAbstract is false).ToList();

        var commands = types.Where(type => type.Name.EndsWith("Command")).ToList();
        var validators = types.Where(type => type.Name.EndsWith("CommandValidator")).ToList();
        
        foreach (var command in commands)
        {
            services.AddTransient(
                serviceType: typeof(ICommandValidator<>).MakeGenericType(command),
                implementationType: validators.First(v => v.Name.StartsWith(command.Name)));
        }

        return services;
    }
}