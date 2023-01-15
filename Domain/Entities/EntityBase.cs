using System.ComponentModel.DataAnnotations;

namespace PlanBee.University_portal.backend.Domain.Entities;

public abstract class EntityBase
{
    [Key]
    public Guid ItemId { get; set; }

    public bool IsMarkedAsDeleted { get; set; } = false;
    
    public bool IsActive { get; set; }

    public DateTime CreatedOn { get; set; }
    
    public DateTime UpdatedOn { get; set; }
}