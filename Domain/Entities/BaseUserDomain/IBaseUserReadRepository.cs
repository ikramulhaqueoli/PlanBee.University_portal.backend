namespace PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

public interface IBaseUserReadRepository
{
    Task<BaseUser?> GetAsync(string baseUserId);

    Task<BaseUser?> GetByCredentialsAsync(string universityId, string passwordHash);
}