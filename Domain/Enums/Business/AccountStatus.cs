using System.Runtime.Serialization;

namespace PlanBee.University_portal.backend.Domain.Enums.Business;

public enum AccountStatus
{
    [EnumMember(Value = "unknown")] Unknown = 0,
    [EnumMember(Value = "verificationsent")] VerificationSent = 10,
    [EnumMember(Value = "verificationsendfail")] VerificationSendFail = 20,
    [EnumMember(Value = "verified")] Verified = 30,
    [EnumMember(Value = "deactive")] Deactive = 40
}