using Microsoft.EntityFrameworkCore;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

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

    public DbSet<RegistrationRequest> RegistrationRequests { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddConstraints();
    }
}