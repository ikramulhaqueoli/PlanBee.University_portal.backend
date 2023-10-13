using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

public interface IRegistrationRequestReadRepository
{
    Task<RegistrationRequest?> GetPendingAsync(string registrationRequestId);

    Task<List<object>> GetWithViewsAsync(
        string[]? specificItemIds,
        UserType userType);
}