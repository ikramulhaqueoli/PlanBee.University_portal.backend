namespace PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

public interface IRegistrationRequestReadRepository
{
    Task<RegistrationRequest> GetAsync(string registrationRequestId);
}