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
    public string? DateOfBirth { get; set; }
    public UserRoles UserRole { get; set; }
    public string? UniversityEmail { get; set; }

    public void Initiate()
    {
        ItemId = Guid.NewGuid();
        CreatedOn = DateTime.UtcNow;
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
}