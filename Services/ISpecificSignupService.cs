using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Services;

public interface ISpecificSignupService
{
    Task CreateAsync(
        string baseUserId,
        AbstractSignupRequestCommand signupRequestCommand);

    UserType UserType { get; }
}