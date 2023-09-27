using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations;

public class RegistrationRequestRepository : IRegistrationRequestWriteRepository
{
    private readonly IMongoCollection<RegistrationRequest> _registrationRequestCollection;

    public RegistrationRequestRepository(IMongoCollection<RegistrationRequest> registrationRequestCollection)
    {
        _registrationRequestCollection = registrationRequestCollection;
    }

    public Task SaveAsync(RegistrationRequest request)
    {
        return _registrationRequestCollection.InsertOneAsync(request);
    }
}
