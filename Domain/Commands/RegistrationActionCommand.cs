using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using System.Text.Json.Serialization;

namespace PlanBee.University_portal.backend.Domain.Commands
{
    public class RegistrationActionCommand : AbstractCommand
    {
        public string RegistrationRequestId { get; set; } = null!;
        [BsonRepresentation(BsonType.String)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RegistrationActionStatus ActionStatus { get; set; }

        public string ActionComment { get; set; } = string.Empty;
    }
}
