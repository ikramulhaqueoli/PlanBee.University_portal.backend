using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Repositories.Implementations;

public class RegistrationRequestRepository : IRegistrationRequestWriteRepository, IRegistrationRequestReadRepository
{
    private readonly IMongoReadRepository _mongoReadRepository;
    private readonly IMongoWriteRepository _mongoWriteRepository;

    public RegistrationRequestRepository(
        IMongoReadRepository mongoReadRepository,
        IMongoWriteRepository mongoWriteRepository)
    {
        _mongoReadRepository = mongoReadRepository;
        _mongoWriteRepository = mongoWriteRepository;
    }

    public Task<List<RegistrationRequest>> GetAllAsync()
    {
        var filter = Builders<RegistrationRequest>.Filter.Empty;
        return _mongoReadRepository.GetAsync(filter);
    }

    public Task<RegistrationRequest?> GetPendingAsync(string registrationRequestId)
    {
        var filter = Builders<RegistrationRequest>.Filter.Eq(nameof(RegistrationRequest.ItemId), registrationRequestId) &
                     Builders<RegistrationRequest>.Filter.Eq(nameof(RegistrationRequest.ActionStatus), RegistrationActionStatus.Pending);
        return _mongoReadRepository.GetFirstOrDefaultAsync(filter);
    }

    public Task SaveAsync(RegistrationRequest request)
    {
        return _mongoWriteRepository.SaveAsync(request);
    }

    public Task UpdateAsync(RegistrationRequest request)
    {
        return _mongoWriteRepository.UpdateAsync(request);
    }
}
