using PlanBee.University_portal.backend.Domain.Enums;

namespace PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

public class BaseUser : EntityBase
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string RegistrationId { get; set; } = null!;

    public string? SurName { get; set; }
    public string? MobilePhone { get; set; }
    public string? Email { get; set; }
    public Gender Gender { get; set; }
    public string? PasswordHash { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public UserRole[]? UserRoles { get; set; }
    public string? UniversityEmail { get; set; }

    public void Initiate()
    {
        ItemId = Guid.NewGuid();
        CreatedOn = DateTime.UtcNow;
        UserRoles ??= new[] { UserRole.Anonymous };
    }

    public void Update()
    {
        UpdatedOn = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void MarkAsDelete()
    {
        IsActive = false;
        IsMarkedAsDeleted = true;
    }

    public void AddRole(params UserRole[] roles)
    {
        UserRoles ??= Array.Empty<UserRole>();
        var existingRoles = UserRoles.ToList();
        existingRoles.AddRange(roles);
        UserRoles = existingRoles.Distinct().ToArray();
    }
}