using PlanBee.University_portal.backend.CommandHandlers.Responses;
using PlanBee.University_portal.backend.Domain.Commands;

namespace PlanBee.University_portal.backend.CommandHandlers.Implementations.Validators;

public class UserSignupCommandValidator : BaseCommandValidator<UserSignupCommand>
{
    public override ValidationResponse Validate(UserSignupCommand command)
    {
        var response = base.Validate(command);
        return response;
    }
}