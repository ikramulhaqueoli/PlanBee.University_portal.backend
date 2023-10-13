using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Services
{
    public interface IUserSignupService
    {
        Task ApproveSignupRequestAsync(RegistrationRequest registrationRequest);

        Task RequestSignupAsync(
            AbstractSignupRequestCommand command,
            UserType userType);
    }
}
