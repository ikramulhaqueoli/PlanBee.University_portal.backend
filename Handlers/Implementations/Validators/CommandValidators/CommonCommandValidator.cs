using PlanBee.University_portal.backend.Domain.Commands;

namespace PlanBee.University_portal.backend.Handlers.Implementations.Validators.CommandValidators
{
    public class CommonCommandValidator<TCommand> : AbstractCommandValidator<TCommand>
        where TCommand : AbstractCommand
    {
        public override void ValidatePrimary(TCommand command)
        {
        }
    }
}
