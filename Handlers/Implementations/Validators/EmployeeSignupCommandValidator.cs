using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.Validators;

public class EmployeeSignupCommandValidator : BaseCommandValidator<EmployeeSignupCommand>
{
    public override ValidationResponse Validate(EmployeeSignupCommand command)
    {
        var response = base.Validate(command);
        return response;
    }
}