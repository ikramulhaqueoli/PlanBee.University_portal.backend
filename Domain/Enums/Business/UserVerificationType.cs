using System.Runtime.Serialization;

namespace PlanBee.University_portal.backend.Domain.Enums.Business
{
    public enum UserVerificationType
    {
        [EnumMember(Value = "unkwown")] Unknown,
        [EnumMember(Value = "signup")] Signup,
        [EnumMember(Value = "forgotpassword")] ForgotPassword
    }
}
