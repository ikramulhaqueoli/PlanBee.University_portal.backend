using System.Runtime.Serialization;

namespace PlanBee.University_portal.backend.Domain.Enums.Business;

public enum Gender
{
    [EnumMember(Value = "unspecified")] Unspecified = 0,
    [EnumMember(Value = "male")] Male = 1,
    [EnumMember(Value = "female")] Female = 2,
    [EnumMember(Value = "other")] Other = 3
}