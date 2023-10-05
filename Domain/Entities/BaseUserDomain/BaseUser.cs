﻿using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Utils;
using System.Security.Cryptography;

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
    public string Email { get; set; } = null!;
    public Gender Gender { get; set; }
    public string? PasswordHash { get; set; }
    public UserRole[]? UserRoles { get; set; }
    public string PermanentAddress { get; set; } = null!;
    public string PresentAddress { get; set; } = null!;
    public string AlternatePhone { get; set; } = null!;
    public string PersonalEmail { get; set; } = null!;
    public string? UniversityEmail { get; set; }
    public AccountStatus AccountStatus { get; set; }
    public UserType UserType { get; set; }

    public string DisplayName => $"{FirstName} {LastName}";


    public void InitiateUserWithEntityBase(Guid? customUserId = null)
    {
        InitiateEntityBase(customUserId);
        UserRoles ??= new[] { UserRole.Anonymous };
    }

    public void SetAsVerificationSent()
    {
        AccountStatus = AccountStatus.VerificationSent;
    }

    public void SetAsVerificationSendFail()
    {
        AccountStatus = AccountStatus.VerificationSendFail;
    }

    public void SetAsDeactivate()
    {
        AccountStatus = AccountStatus.Deactive;
    }

    public void SetAsVerified()
    {
        AccountStatus = AccountStatus.Verified;
    }

    public void SetPasswordAsHash(string password)
    {
        PasswordHash = password.Md5Hash();
    }

    public void MarkAsDelete()
    {
        AccountStatus = AccountStatus.Deactive;
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