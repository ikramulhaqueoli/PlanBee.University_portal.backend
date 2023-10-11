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
        var primaryValidationResponse = validator!.TryValidatePrimary(command);
        if (primaryValidationResponse.Success is false)
        {
            return primaryValidationResponse;
        }

        var businessValidationResponse = await validator!.TryValidateBusinessAsync(command);
        if (businessValidationResponse.Success is false)
        {
            return businessValidationResponse;
        }

        var handler = _service.GetService<ICommandHandler<TCommand>>();
        return await handler!.TryHandleAsync(command);
    }
}