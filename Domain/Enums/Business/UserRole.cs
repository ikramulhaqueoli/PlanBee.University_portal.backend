using System.Runtime.Serialization;

namespace PlanBee.University_portal.backend.Domain.Enums.Business;

public enum UserRole
{
    [EnumMember(Value = "none")] None = 0,
    [EnumMember(Value = "anonymous")] Anonymous = 100,
    [EnumMember(Value = "clerk")] Clerk = 200,
    [EnumMember(Value = "generalemployee")] GeneralEmployee = 300,
    [EnumMember(Value = "student")] Student = 400,
    [EnumMember(Value = "facultymember")] FacultyMember = 500,
    [EnumMember(Value = "departmenthead")] DepartmentHead = 600,
    [EnumMember(Value = "dean")] Dean = 700,
    [EnumMember(Value = "vicechancellor")] ViceChancellor = 800,
    [EnumMember(Value = "moderator")] Moderator = 900,
    [EnumMember(Value = "superadmin")] SuperAdmin = 1000
}
