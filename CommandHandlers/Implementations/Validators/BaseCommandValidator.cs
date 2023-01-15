using PlanBee.University_portal.backend.CommandHandlers.Responses;
using PlanBee.University_portal.backend.Domain.Commands;

namespace PlanBee.University_portal.backend.CommandHandlers.Implementations.Validators;

public class BaseCommandValidator<TCommand>
    : ICommandValidator<TCommand> where TCommand : AbstractCommand
{
    public virtual ValidationResponse Validate(TCommand command)
    {
        return new ValidationResponse();
    }
}