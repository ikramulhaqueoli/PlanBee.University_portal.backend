using PlanBee.University_portal.backend.CommandHandlers.Responses;
using PlanBee.University_portal.backend.Domain.Commands;

namespace PlanBee.University_portal.backend.CommandHandlers.Implementations;

public class CommandDispatcher : ICommandDispatcher
{
    public async Task<AbstractResponse> DispatchAsync<TCommand>(TCommand command)
        where TCommand : AbstractCommand
    {
        var validator = Activator.CreateInstance<ICommandValidator<TCommand>>();
        var response = validator.Validate(command);

        if (response.Success is false) return response;

        var handler = Activator.CreateInstance<ICommandHandler<TCommand>>();
        await handler.HandleAsync(command);
        return response;
    }
}