using PlanBee.University_portal.backend.CommandHandlers.Responses;
using PlanBee.University_portal.backend.Domain.Commands;

namespace PlanBee.University_portal.backend.CommandHandlers;

public interface ICommandDispatcher
{
    Task<AbstractResponse> DispatchAsync<TCommand>(TCommand command)
        where TCommand : AbstractCommand;
}