﻿using PlanBee.University_portal.backend.Domain.Enums;

namespace PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

public class BaseUser : EntityBase
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? NickName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public Gender Gender { get; set; }
    public string? RegistrationId { get; set; }
    public string? PasswordHash { get; set; }
    public string? DateOfBirth { get; set; }
    public UserRoles UserRole { get; set; }
    public string? UniversityEmail { get; set; }
}