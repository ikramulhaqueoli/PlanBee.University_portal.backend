using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers;

public interface ICommandValidator<in TCommand>
    where TCommand : AbstractCommand
{
    CommandResponse TryValidatePrimary(TCommand command);

    void ValidatePrimary(TCommand command);
}