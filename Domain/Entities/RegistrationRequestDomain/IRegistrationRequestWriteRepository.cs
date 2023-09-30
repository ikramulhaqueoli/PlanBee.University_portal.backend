namespace PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

public interface IRegistrationRequestWriteRepository
{
    Task SaveAsync(RegistrationRequest request);

    Task UpdateAsync(RegistrationRequest request);
}