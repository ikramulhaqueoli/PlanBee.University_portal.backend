using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using System.Text.Json.Serialization;

namespace PlanBee.University_portal.backend.Domain.Entities.StudentDomain
{
    public class Student : EntityBase
    {
        public DateTime AdmissionDate { get; set; }

        public string DepartmentId { get; set; } = null!;

        [BsonRepresentation(BsonType.String)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RecidenceStatus RecidenceStatus { get; set; }

        public string BaseUserId { get; set; } = null!;
    }
}
