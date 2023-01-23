using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers;

public interface ICommandDispatcher
{
    Task<AbstractResponse> DispatchAsync<TCommand>(TCommand command)
        where TCommand : AbstractCommand;
}