using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Handlers.Responses;

namespace PlanBee.University_portal.backend.Handlers;

public interface ICommandValidator<in TCommand>
    where TCommand : AbstractCommand
{
    ValidationResponse Validate(TCommand command);
}