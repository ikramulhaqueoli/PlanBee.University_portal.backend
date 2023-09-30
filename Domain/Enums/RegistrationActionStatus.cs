using System.Runtime.Serialization;

namespace PlanBee.University_portal.backend.Domain.Enums;

public enum RegistrationActionStatus
{
    [EnumMember(Value = "none")] None,
    [EnumMember(Value = "pending")] Pending,
    [EnumMember(Value = "approved")] Approved,
    [EnumMember(Value = "rejected")] Rejected
}