using System.ComponentModel.DataAnnotations;

namespace PlanBee.University_portal.backend.Domain.Entities;

public abstract class EntityBase
{
    [Key]
    public Guid EntityId { get; set; }
}