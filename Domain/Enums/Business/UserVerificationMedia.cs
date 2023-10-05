using System.Runtime.Serialization;

namespace PlanBee.University_portal.backend.Domain.Enums.Business
{
    public enum UserVerificationMedia
    {
        [EnumMember(Value = "unknown")] Unknown,
        [EnumMember(Value = "email")] Email,
        [EnumMember(Value = "phonecall")] PhoneCall,
        [EnumMember(Value = "phonesms")] PhoneSms,
    }
}
