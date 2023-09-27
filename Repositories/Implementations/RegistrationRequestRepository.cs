using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations;

public class RegistrationRequestRepository : IRegistrationRequestWriteRepository
{
    private readonly IMongoCollection<RegistrationRequest> _registrationRequestCollection;

    public RegistrationRequestRepository(IMongoDbCollectionProvider mongoDbCollectionProvider)
    {
        _registrationRequestCollection = mongoDbCollectionProvider.getCollection<RegistrationRequest>();
    }

    public Task SaveAsync(RegistrationRequest request)
    {
        return _registrationRequestCollection.InsertOneAsync(request);
    }
}
