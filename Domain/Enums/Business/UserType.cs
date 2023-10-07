using System.Runtime.Serialization;

namespace PlanBee.University_portal.backend.Domain.Enums.Business
{
    public enum UserType
    {
        [EnumMember(Value = "unknown")] Unknown,
        [EnumMember(Value = "employee")] Employee,
        [EnumMember(Value = "student")] Student,
    }
}