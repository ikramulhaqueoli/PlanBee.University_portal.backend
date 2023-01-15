using Microsoft.Extensions.DependencyInjection;
using PlanBee.University_portal.backend.CommandHandlers.Responses;
using PlanBee.University_portal.backend.Domain.Commands;

namespace PlanBee.University_portal.backend.CommandHandlers.Implementations;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _service;

    public CommandDispatcher(IServiceProvider service)
    {
        _service = service;
    }

    public async Task<AbstractResponse> DispatchAsync<TCommand>(TCommand command)
        where TCommand : AbstractCommand
    {
        var validator = _service.GetService<ICommandValidator<TCommand>>();
        var response = validator!.Validate(command);

        if (response.Success is false) return response;

        var handler = _service.GetService<ICommandHandler<TCommand>>();
        await handler!.HandleAsync(command);
        return response;
    }
}