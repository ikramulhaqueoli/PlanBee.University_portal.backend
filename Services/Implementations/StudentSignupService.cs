using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

namespace PlanBee.University_portal.backend.Services.Implementations
{
    internal class StudentSignupService : IStudentSignupService
    {
        public Task ApproveSignupRequest(RegistrationRequest registrationRequest)
        {
            throw new NotImplementedException();
        }

        public Task SignupAsync(StudentSignupCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
