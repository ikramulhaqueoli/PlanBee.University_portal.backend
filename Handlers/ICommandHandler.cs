using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers;

public interface ICommandHandler<in TCommand>
    where TCommand : AbstractCommand
{
    Task<CommandResponse> TryHandleAsync(TCommand command);

    protected Task<CommandResponse> HandleAsync(TCommand command);
}