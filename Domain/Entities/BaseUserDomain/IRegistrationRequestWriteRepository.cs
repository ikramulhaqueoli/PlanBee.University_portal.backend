using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

namespace PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

public interface IRegistrationRequestWriteRepository
{
    Task SaveAsync(RegistrationRequest request);
}