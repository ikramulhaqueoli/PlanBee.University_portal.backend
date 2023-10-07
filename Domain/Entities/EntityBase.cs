using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PlanBee.University_portal.backend.Domain.Entities;

public abstract class EntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string ItemId { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsMarkedAsDeleted { get; set; } = false;

    public DateTime CreatedOn { get; set; }

    public DateTime LastModifiedOn { get; set; }

    public void InitiateEntityBase(Guid? customItemId = null)
    {
        ItemId = (customItemId ?? Guid.NewGuid()).ToString();
        CreatedOn = DateTime.UtcNow;
        IsActive = true;
    }

    public void MarkAsInactive()
    {
        IsActive = false;
    }

    public void Modify()
    {
        LastModifiedOn = DateTime.UtcNow;
    }
}
