namespace PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

public interface IBaseUserReadRepository
{
    Task<BaseUser?> GetByCredentialsAsync(string universityId, string passwordHash);
}