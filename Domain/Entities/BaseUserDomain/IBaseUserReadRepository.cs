namespace PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

public interface IBaseUserReadRepository
{
    Task<BaseUser?> GetAsync(string baseUserId);

    Task<List<BaseUser>> GetManyAsync(List<string> baseUserIds);

    Task<BaseUser?> GetByCredentialsAsync(
        string emailOrUniversityId,
        string passwordHash);

    Task<bool> UserExistsAsync(Guid baseUserId);
}