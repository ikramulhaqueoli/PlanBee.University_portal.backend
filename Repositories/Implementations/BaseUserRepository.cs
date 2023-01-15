using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations;

public class BaseUserRepository : 
    IBaseUserReadRepository, 
    IBaseUserWriteRepository
{
    private readonly UniversityDbContext _universityDbContext;

    public BaseUserRepository(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task SaveAsync(BaseUser user)
    {
        _universityDbContext.BaseUsers.Add(user);
        await _universityDbContext.SaveChangesAsync();
    }
}