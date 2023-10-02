using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

public class BaseUser : EntityBase
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FatherName { get; set; } = null!;
    public string MotherName { get; set; } = null!;
    public string MobilePhone { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string UniversityId { get; set; } = null!;
    public string? NationalId { get; set; }
    public string? PassportNo { get; set; }
    public string? SurName { get; set; }
    public string? Email { get; set; }
    public Gender Gender { get; set; }
    public string? PasswordHash { get; set; }
    public UserRole[]? UserRoles { get; set; }
    public string PermanentAddress { get; set; } = null!;
    public string PresentAddress { get; set; } = null!;
    public string AlternatePhone { get; set; } = null!;
    public string PersonalEmail { get; set; } = null!;
    public string? UniversityEmail { get; set; }
    public bool IsActive { get; set; }
    public UserType UserType { get; set; }


    public void InitiateUserWithEntityBase(Guid? customUserId = null)
    {
        InitiateEntityBase(customUserId);
        UserRoles ??= new[] { UserRole.Anonymous };
    }

    public void Modify()
    {
        LastModifiedOn = DateTime.UtcNow;
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