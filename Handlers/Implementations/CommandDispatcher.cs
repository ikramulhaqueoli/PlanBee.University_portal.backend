using Microsoft.Extensions.DependencyInjection;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations;

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
        var validationResponse = validator!.TryValidatePrimary(command);
        if (validationResponse.Success is false)
        {
            return validationResponse;
        }

        var handler = _service.GetService<ICommandHandler<TCommand>>();
        return await handler!.TryHandleAsync(command);
    }
}