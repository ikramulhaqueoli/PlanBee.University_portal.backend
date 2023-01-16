using Microsoft.EntityFrameworkCore;
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

    public async Task<BaseUser?> GetByCredentialsAsync(string registrationId, string passwordHash)
    {
        return await _universityDbContext.BaseUsers.FirstOrDefaultAsync(user =>
            user.RegistrationId == registrationId &&
            user.PasswordHash == passwordHash &&
            user.IsMarkedAsDeleted == false);
    }

    public async Task SaveAsync(BaseUser user)
    {
        _universityDbContext.BaseUsers.Add(user);
        await _universityDbContext.SaveChangesAsync();
    }
}