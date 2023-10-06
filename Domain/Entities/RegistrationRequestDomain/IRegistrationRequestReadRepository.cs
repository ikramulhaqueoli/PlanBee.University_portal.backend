namespace PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

public interface IRegistrationRequestReadRepository
{
    Task<RegistrationRequest?> GetPendingAsync(string registrationRequestId);

    Task<List<RegistrationRequest>> GetAllAsync();
}