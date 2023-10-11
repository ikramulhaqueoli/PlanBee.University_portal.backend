using Microsoft.Extensions.DependencyInjection;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Exceptions.SystemExceptions;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Handlers.Implementations;
using PlanBee.University_portal.backend.Handlers.Implementations.Validators.CommandValidators;
using PlanBee.University_portal.backend.Handlers.Implementations.Validators.QueryValidators;
using System.Data;

namespace PlanBee.University_portal.backend.Handlers;

public static class ServiceCollectionExtensions
{
    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();

        services.AddCommandHandlers();
        services.AddQueryHandlers();

        services.AddCommandValidators();
        services.AddQueryValidators();
    }

    private static void AddCommandHandlers(this IServiceCollection services)
    {
        var handlerTypes = typeof(AbstractCommandValidator<>).Assembly.GetTypes().Where(type =>
            type.IsAbstract is false &&
            type.Name.EndsWith("CommandHandler")).ToList();

        var commandTypes = typeof(AbstractCommand).Assembly.GetTypes().Where(type =>
            type.IsAbstract is false &&
            type.Name.EndsWith("Command")).ToList();

        foreach (var commandType in commandTypes)
        {
            var handler = handlerTypes.FirstOrDefault(type => type.Name.StartsWith(commandType.Name));
            if (handler == null) throw new CommandHandlerNotFoundException(commandType);

            services.AddScoped(
                typeof(ICommandHandler<>).MakeGenericType(commandType),
                handler);
        }
    }

    private static void AddQueryHandlers(this IServiceCollection services)
    {
        var handlerTypes = typeof(AbstractQueryValidator<>).Assembly.GetTypes().Where(type =>
            type.IsAbstract is false &&
            type.Name.EndsWith("QueryHandler")).ToList();

        var queryTypes = typeof(AbstractQuery).Assembly.GetTypes().Where(type =>
            type.IsAbstract is false &&
            type.Name.EndsWith("Query")).ToList();

        foreach (var queryType in queryTypes)
        {
            var handler = handlerTypes.FirstOrDefault(type => type.Name.StartsWith(queryType.Name));
            if (handler == null) throw new QueryHandlerNotFoundException(queryType);

            services.AddScoped(
                typeof(IQueryHandler<>).MakeGenericType(queryType),
                handler);
        }
    }

    private static void AddCommandValidators(this IServiceCollection services)
    {
        var validatorTypes = typeof(AbstractCommandValidator<>).Assembly.GetTypes().Where(type =>
            type.IsAbstract is false &&
            type.Name.EndsWith("CommandValidator")).ToList();

        var commandTypes = typeof(AbstractCommand).Assembly.GetTypes().Where(type =>
            type.IsAbstract is false &&
            type.Name.EndsWith("Command")).ToList();

        foreach (var commandType in commandTypes)
        {
            var validator = validatorTypes.FirstOrDefault(type => type.Name.StartsWith(commandType.Name)) ??
                            typeof(CommonCommandValidator<>).MakeGenericType(commandType);

            services.AddSingleton(
                typeof(ICommandValidator<>).MakeGenericType(commandType),
                validator);
        }
    }

    private static void AddQueryValidators(this IServiceCollection services)
    {
        var validatorTypes = typeof(AbstractQueryValidator<>).Assembly.GetTypes().Where(type =>
            type.IsAbstract is false &&
            type.Name.EndsWith("QueryValidator")).ToList();

        var queryTypes = typeof(AbstractQuery).Assembly.GetTypes().Where(type =>
            type.IsAbstract is false &&
            type.Name.EndsWith("Query")).ToList();

        foreach (var queryType in queryTypes)
        {
            var validator = validatorTypes.FirstOrDefault(type => type.Name.StartsWith(queryType.Name)) ??
                            typeof(CommonQueryValidator<>).MakeGenericType(queryType);

            services.AddSingleton(
                typeof(IQueryValidator<>).MakeGenericType(queryType),
                validator);
        }
    }
}