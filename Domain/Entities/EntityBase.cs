using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace PlanBee.University_portal.backend.Domain.Entities;

public abstract class EntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string ItemId { get; set; } = null!;

    public bool IsActive { get; set; }

    [JsonIgnore]
    public bool IsMarkedAsDeleted { get; set; } = false;

    [JsonIgnore]
    public DateTime CreatedOn { get; set; }

    [JsonIgnore]
    public DateTime LastModifiedOn { get; set; }

    public void InitiateEntityBase(string? customItemId = null)
    {
        ItemId = customItemId ?? Guid.NewGuid().ToString();
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
