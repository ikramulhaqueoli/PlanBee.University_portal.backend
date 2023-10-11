using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations;

public class BaseUserRepository :
    IBaseUserReadRepository,
    IBaseUserWriteRepository
{
    private readonly IMongoReadRepository _mongoReadRepository;
    private readonly IMongoWriteRepository _mongoWriteRepository;

    public BaseUserRepository(
        IMongoReadRepository mongoReadRepository,
        IMongoWriteRepository mongoWriteRepository)
    {
        _mongoReadRepository = mongoReadRepository;
        _mongoWriteRepository = mongoWriteRepository;
    }

    public Task<BaseUser?> GetAsync(string baseUserId)
    {
        var filter = Builders<BaseUser>.Filter.Eq(nameof(BaseUser.ItemId), baseUserId);
        return _mongoReadRepository.GetFirstOrDefaultAsync(filter);
    }

    public async Task<BaseUser?> GetByCredentialsAsync(
        string emailOrUniversityId,
        string passwordHash)
    {
        var filter = Builders<BaseUser>.Filter.Where(user =>
            (user.UniversityId == emailOrUniversityId || 
             user.PersonalEmail == emailOrUniversityId || 
             user.UniversityEmail == emailOrUniversityId) &&
            user.PasswordHash == passwordHash
        );

        return await _mongoReadRepository.GetFirstOrDefaultAsync(filter);
    }

    public Task SaveAsync(BaseUser user)
    {
        return _mongoWriteRepository.SaveAsync(user);
    }

    public Task UpdateAsync(BaseUser user)
    {
        return _mongoWriteRepository.UpdateAsync(user);
    }

    public async Task<bool> UserExistsAsync(Guid baseUserId)
    {
        var filter = Builders<BaseUser>.Filter.Eq(nameof(BaseUser.ItemId), baseUserId);
        var count = await _mongoReadRepository.GetCountAsync(filter);
        return count > 0;
    }
}