using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Handlers.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.Validators;

public class UserSignupCommandValidator : BaseCommandValidator<UserSignupCommand>
{
    public override ValidationResponse Validate(UserSignupCommand command)
    {
        var response = base.Validate(command);
        return response;
    }
}