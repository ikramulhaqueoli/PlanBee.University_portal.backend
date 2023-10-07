using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using System.Text.Json;

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

    public async Task<List<object>> GetAllWithViewAsync()
    {
        var filter = Builders<RegistrationRequest>.Filter.Empty;
        var results = await _mongoReadRepository.GetAsync(filter);

        return results.Select(result => new
        {
            RegistrationRequest = result,
            SignupData = JsonDocument.Parse(result.CommandJson)
        }).ToList<object>();
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
