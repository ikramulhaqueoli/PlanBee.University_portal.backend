using Microsoft.EntityFrameworkCore;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

namespace PlanBee.University_portal.backend.Repositories;

public static class ModelBuilderExtensions
{
    internal static void AddConstraints(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaseUser>().HasIndex(user => new { user.Email }).IsUnique();
        modelBuilder.Entity<BaseUser>().HasIndex(user => new { Phone = user.MobilePhone }).IsUnique();
        modelBuilder.Entity<BaseUser>().HasIndex(user => new { user.RegistrationId }).IsUnique();
    }
}