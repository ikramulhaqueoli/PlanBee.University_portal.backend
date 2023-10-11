namespace PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

public interface IBaseUserReadRepository
{
    Task<BaseUser?> GetAsync(string baseUserId);

    Task<BaseUser?> GetByCredentialsAsync(
        string emailOrUniversityId,
        string passwordHash);

    Task<bool> UserExistsAsync(Guid baseUserId);
}