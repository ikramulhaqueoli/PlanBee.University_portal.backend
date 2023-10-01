using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations;

public class BaseUserRepository :
    IBaseUserReadRepository,
    IBaseUserWriteRepository
{
    private readonly IMongoReadRepository _mongoReadRepository;
    private readonly IMongoWriteRepository _mongoWriteRepository;

    public BaseUserRepository(IMongoReadRepository mongoReadRepository, IMongoWriteRepository mongoWriteRepository)
    {
        _mongoReadRepository = mongoReadRepository;
        _mongoWriteRepository = mongoWriteRepository;
    }

    public async Task<BaseUser?> GetByCredentialsAsync(string universityId, string passwordHash)
    {
        var filter = Builders<BaseUser>.Filter.Where(user =>
            user.UniversityId == universityId &&
            user.PasswordHash == passwordHash
        );

        return await _mongoReadRepository.GetFirstOrDefaultAsync(filter);
    }

    public Task SaveAsync(BaseUser user)
    {
        return _mongoWriteRepository.SaveAsync(user);
    }
}