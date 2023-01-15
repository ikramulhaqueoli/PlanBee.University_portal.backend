using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations;

public class RegistrationRequestRepository : IRegistrationRequestWriteRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public RegistrationRequestRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task SaveAsync(RegistrationRequest request)
    {
        _universityDbContext.RegistrationRequests.Add(request);
        await _universityDbContext.SaveChangesAsync();
    }
}