using System.Runtime.Serialization;

namespace PlanBee.University_portal.backend.Domain.Enums;

public enum UserRole
{
    [EnumMember(Value = "super_admin")] SuperAdmin = 0,
    [EnumMember(Value = "moderator")] Moderator = 10,
    [EnumMember(Value = "vice_chancellor")] ViceChancellor = 20,
    [EnumMember(Value = "dean")] Dean = 30,
    [EnumMember(Value = "department_head")] DepartmentHead = 40,
    [EnumMember(Value = "faculty_member")] FacultyMember = 50,
    [EnumMember(Value = "student")] Student = 60,
    [EnumMember(Value = "general_employee")] GeneralEmployee = 70,
    [EnumMember(Value = "clerk")] Clerk = 100,
    [EnumMember(Value = "anonymous")] Anonymous = 1000
}