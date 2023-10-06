using PlanBee.University_portal.backend.Domain.Constants;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Utils;

namespace PlanBee.University_portal.backend.Domain.Entities.UserVerificationDomain
{
    public class UserVerification : EntityBase
    {
        public string BaseUserId { get; set; } = null!;

        public string VerificationCode { get; set; } = null!;

        public DateTime ValidUntilUtc { get; set; }

        public UserVerificationType VerificationType { get; set; }

        public UserVerificationMedia VerificationMedia { get; set; }

        public void SetValidityByDays(int day)
        {
            ValidUntilUtc = DateTime.UtcNow.AddDays(day);
        }

        public bool IsStillValid() => ValidUntilUtc >= DateTime.UtcNow;

        public void SetVerificationCode()
        {
            VerificationCode = ItemId.Replace("-", "");
        }

        public void Initiate(UserVerificationType verificationType, string baseUserId)
        {
            InitiateEntityBase();
            BaseUserId = baseUserId;
            VerificationType = verificationType;
            VerificationMedia = UserVerificationMedia.Email;
            SetValidityByDays(BusinessConstants.VERIFI_CODE_VALIDITY_DAYS);
            SetVerificationCode();
        }
    }
}
