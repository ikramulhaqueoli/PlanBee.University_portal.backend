using Microsoft.EntityFrameworkCore;
using PlanBee.University_portal.backend.Domain.Entities;

namespace PlanBee.University_portal.backend.Repositories;

public partial class UniversityDb
{
    public DbSet<BaseUser> BaseUsers { get; set; } = null!;
}