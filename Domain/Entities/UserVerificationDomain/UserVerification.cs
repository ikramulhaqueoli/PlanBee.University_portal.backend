using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Utils;

namespace PlanBee.University_portal.backend.Domain.Entities.UserVerificationDomain
{
    public class UserVerification : EntityBase
    {
        public string BaseUserId { get; set; } = null!;

        public string VerificationCode => UserVerificationUtils.GetCodeFromItemId(ItemId);

        public DateTime ValidUntilUtc { get; set; }

        public UserVerificationType VerificationType { get; set; }

        public UserVerificationMedia VerificationMedia { get; set; }

        public void SetValidityByDays(int day)
        {
            ValidUntilUtc = DateTime.UtcNow.AddDays(day);
        }

        public bool IsStillValid() => ValidUntilUtc >= DateTime.UtcNow;
    }
}
