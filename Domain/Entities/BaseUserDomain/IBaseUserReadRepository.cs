namespace PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

public interface IBaseUserReadRepository
{
    Task<bool> IsCredentialsValidAsync(string registrationId, string passwordHash);
}