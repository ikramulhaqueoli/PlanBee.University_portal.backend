using PlanBee.University_portal.backend.CommandHandlers.Responses;
using PlanBee.University_portal.backend.Domain.Commands;

namespace PlanBee.University_portal.backend.CommandHandlers;

public interface ICommandValidator<in TCommand>
    where TCommand : AbstractCommand
{
    ValidationResponse Validate(TCommand command);
}