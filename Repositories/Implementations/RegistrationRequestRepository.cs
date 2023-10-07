using MongoDB.Driver;
using Newtonsoft.Json;
using PlanBee.University_portal.backend.Domain.Commands;
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

    public async Task<List<RegistrationRequest>> GetAllAsync()
    {
        var filter = Builders<RegistrationRequest>.Filter.Empty;
        var results = await _mongoReadRepository.GetAsync(filter);

        foreach (var result in results)
        {
            result.CommandModel = null;
            if (result.UserType == UserType.Employee)
                result.CommandModel = JsonConvert.DeserializeObject<EmployeeSignupCommand>(result.CommandJson);
            else if (result.UserType == UserType.Student)
                result.CommandModel = JsonConvert.DeserializeObject<StudentSignupCommand>(result.CommandJson);
            else result.CommandModel = null;
        }

        return results;
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
