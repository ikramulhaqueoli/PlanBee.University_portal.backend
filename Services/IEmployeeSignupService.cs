using PlanBee.University_portal.backend.Domain.Commands;

namespace PlanBee.University_portal.backend.Services;

public interface IEmployeeSignupService
{
    Task SignupAsync(EmployeeSignupCommand command);
}