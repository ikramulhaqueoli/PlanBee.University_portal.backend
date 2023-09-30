using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

namespace PlanBee.University_portal.backend.Services;

public interface IEmployeeSignupService
{
    Task SignupAsync(EmployeeSignupCommand command);

    Task ApproveSignupRequest(RegistrationRequest registrationRequest);
}