using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PlanBee.University_portal.backend.Domain.Constants;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using System.Text.Json.Serialization;

namespace PlanBee.University_portal.backend.Domain.Entities.UserVerificationDomain
{
    public class UserVerification : EntityBase
    {
        public string BaseUserId { get; set; } = null!;

        public string VerificationCode { get; set; } = null!;

        public DateTime ValidUntilUtc { get; set; }

        [BsonRepresentation(BsonType.String)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserVerificationType VerificationType { get; set; }

        [BsonRepresentation(BsonType.String)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
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

        public void Initiate(UserVerificationType verificationType, string baseUserId, string creatorUserId)
        {
            InitiateEntityBase(creatorUserId);
            BaseUserId = baseUserId;
            VerificationType = verificationType;
            VerificationMedia = UserVerificationMedia.Email;
            SetValidityByDays(BusinessConstants.VERIFI_CODE_VALIDITY_DAYS);
            SetVerificationCode();
        }
    }
}
