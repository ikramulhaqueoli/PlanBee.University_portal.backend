using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Handlers.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.Validators;

public class BaseCommandValidator<TCommand>
    : ICommandValidator<TCommand> where TCommand : AbstractCommand
{
    public virtual ValidationResponse Validate(TCommand command)
    {
        return new ValidationResponse();
    }
}