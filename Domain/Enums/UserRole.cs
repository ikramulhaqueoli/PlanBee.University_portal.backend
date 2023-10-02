using System.Runtime.Serialization;

namespace PlanBee.University_portal.backend.Domain.Enums;

public enum UserRole
{
    [EnumMember(Value = "SuperAdmin")] SuperAdmin = 0,
    [EnumMember(Value = "Moderator")] Moderator = 10,
    [EnumMember(Value = "ViceChancellor")] ViceChancellor = 20,
    [EnumMember(Value = "Dean")] Dean = 30,
    [EnumMember(Value = "DepartmentHead")] DepartmentHead = 40,
    [EnumMember(Value = "FacultyMember")] FacultyMember = 50,
    [EnumMember(Value = "Student")] Student = 60,
    [EnumMember(Value = "GeneralEmployee")] GeneralEmployee = 70,
    [EnumMember(Value = "Clerk")] Clerk = 100,
    [EnumMember(Value = "Anonymous")] Anonymous = 1000
}