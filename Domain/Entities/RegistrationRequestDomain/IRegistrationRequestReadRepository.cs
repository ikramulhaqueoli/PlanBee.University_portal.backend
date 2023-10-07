using System.Text.Json;

namespace PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

public interface IRegistrationRequestReadRepository
{
    Task<RegistrationRequest?> GetPendingAsync(string registrationRequestId);

    Task<List<object>> GetAllWithViewAsync();
}