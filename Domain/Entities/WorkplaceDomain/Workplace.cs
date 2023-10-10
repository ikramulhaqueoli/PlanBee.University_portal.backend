using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using System.Text.Json.Serialization;

namespace PlanBee.University_portal.backend.Domain.Entities.WorkplaceDomain
{
    public class Workplace : EntityBase
    {
        [BsonRepresentation(BsonType.String)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WorkplaceType WorkplaceType { get; set; }

        public string WorkplaceTitle { get; set; } = null!;

        public string WorkplaceAcronym { get; set; } = null!;
    }
}
