using System.Runtime.Serialization;

namespace PlanBee.University_portal.backend.Domain.Enums.Business;

public enum RegistrationActionStatus
{
    [EnumMember(Value = "pending")] Pending,
    [EnumMember(Value = "approved")] Approved,
    [EnumMember(Value = "rejected")] Rejected
}