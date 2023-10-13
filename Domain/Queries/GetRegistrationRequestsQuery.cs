using PlanBee.University_portal.backend.Domain.Enums.Business;
using System.Text.Json.Serialization;

namespace PlanBee.University_portal.backend.Domain.Queries
{
    public class GetRegistrationRequestsQuery : AbstractQuery
    {
        public string[]? SpecificItemIds { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserType UserType { get; set; }
    }
}
