using System.Runtime.Serialization;

namespace PlanBee.University_portal.backend.Domain.Enums
{
    public enum UserType
    {
        [EnumMember(Value = "unknown")] Unknown,
        [EnumMember(Value = "employee")] Employee,
    }
}