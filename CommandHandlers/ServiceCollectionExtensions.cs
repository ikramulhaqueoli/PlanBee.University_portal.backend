using Microsoft.Extensions.DependencyInjection;
using PlanBee.University_portal.backend.CommandHandlers.Implementations;
using PlanBee.University_portal.backend.CommandHandlers.Implementations.Validators;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Exceptions;

namespace PlanBee.University_portal.backend.CommandHandlers;

public static class ServiceCollectionExtensions
{
    public static void AddCommandHandlers(this IServiceCollection services)
    {
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddHandlers();
        services.AddValidators();
    }

    private static void AddHandlers(this IServiceCollection services)
    {
        var handlerTypes = typeof(BaseCommandValidator<>).Assembly.GetTypes().Where(type => 
            type.IsAbstract is false && 
            type.Name.EndsWith("CommandHandler")).ToList();

        var commandTypes = typeof(AbstractCommand).Assembly.GetTypes().Where(type => 
            type.IsAbstract is false && 
            type.Name.EndsWith("Command")).ToList();
        
        foreach (var commandType in commandTypes)
        {
            var handler = handlerTypes.FirstOrDefault(type => type.Name.StartsWith(commandType.Name));
            if (handler == null) throw new HandlerNotFoundException(commandType);
            
            services.AddScoped(
                serviceType: typeof(ICommandHandler<>).MakeGenericType(commandType),
                implementationType: handler);
        }
    }

    private static void AddValidators(this IServiceCollection services)
    {
        var validatorTypes = typeof(BaseCommandValidator<>).Assembly.GetTypes().Where(type => 
            type.IsAbstract is false && 
            type.Name.EndsWith("CommandValidator")).ToList();

        var commandTypes = typeof(AbstractCommand).Assembly.GetTypes().Where(type => 
            type.IsAbstract is false && 
            type.Name.EndsWith("Command")).ToList();
        
        foreach (var commandType in commandTypes)
        {
            var validator = validatorTypes.FirstOrDefault(type => type.Name.StartsWith(commandType.Name)) ??
                            typeof(BaseCommandValidator<>).MakeGenericType(commandType);
            
            services.AddSingleton(
                serviceType: typeof(ICommandValidator<>).MakeGenericType(commandType),
                implementationType: validator);
        }
    }
}