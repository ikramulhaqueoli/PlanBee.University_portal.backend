using System.Runtime.Serialization;

namespace PlanBee.University_portal.backend.Domain.Enums.Business;

public enum UserRole
{
    [EnumMember(Value = "superadmin")] SuperAdmin = 0,
    [EnumMember(Value = "moderator")] Moderator = 10,
    [EnumMember(Value = "vicechancellor")] ViceChancellor = 20,
    [EnumMember(Value = "dean")] Dean = 30,
    [EnumMember(Value = "departmenthead")] DepartmentHead = 40,
    [EnumMember(Value = "facultymember")] FacultyMember = 50,
    [EnumMember(Value = "student")] Student = 60,
    [EnumMember(Value = "generalemployee")] GeneralEmployee = 70,
    [EnumMember(Value = "clerk")] Clerk = 100,
    [EnumMember(Value = "anonymous")] Anonymous = 1000
}