using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PlanBee.University_portal.backend.Domain.Entities;

public abstract class EntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ItemId { get; set; } = null!;

    public bool IsMarkedAsDeleted { get; set; } = false;

    public bool IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }
}
