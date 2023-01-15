using Microsoft.EntityFrameworkCore;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

namespace PlanBee.University_portal.backend.Repositories;

public class UniversityDbContext : DbContext
{
    public UniversityDbContext()
    {
    }
    
    public UniversityDbContext(DbContextOptions options)
        : base(options)
    {
    }
    
    public DbSet<BaseUser> BaseUsers { get; set; } = null!;
}