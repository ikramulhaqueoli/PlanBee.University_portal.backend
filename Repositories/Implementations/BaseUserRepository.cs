using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations;

public class BaseUserRepository :
    IBaseUserReadRepository,
    IBaseUserWriteRepository
{
    private readonly IMongoCollection<BaseUser> _baseUserCollection;

    public BaseUserRepository(IMongoCollection<BaseUser> baseUserCollection)
    {
        _baseUserCollection = baseUserCollection;
    }

    public Task<BaseUser?> GetByCredentialsAsync(string registrationId, string passwordHash)
    {
        IMongoDbCollectionProvider collectionProvider;
        return Task.FromResult<BaseUser?>(
            _baseUserCollection.Find(user => 
                user.RegistrationId == registrationId &&
                user.PasswordHash == passwordHash && 
                !user.IsMarkedAsDeleted)
            .FirstOrDefault()
            );
    }

    public Task SaveAsync(BaseUser user)
    {
        return _baseUserCollection.InsertOneAsync(user);
    }
}