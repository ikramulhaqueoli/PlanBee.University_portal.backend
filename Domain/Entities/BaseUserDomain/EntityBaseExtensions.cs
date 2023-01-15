namespace PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

public static class EntityBaseExtensions
{
    public static void Initiate(this BaseUser user)
    {
        user.EntityId = Guid.NewGuid();
        user.CreatedOn = DateTime.UtcNow;
        user.Activate();
    }
    
    public static void Update(this BaseUser user)
    {
        user.UpdatedOn = DateTime.UtcNow;
    }
    
    public static void Activate(this BaseUser user)
    {
        user.IsActive = true;
    }
    
    public static void Deactivate(this BaseUser user)
    {
        user.IsActive = false;
    }
    
    public static void MarkAsDelete(this BaseUser user)
    {
        user.IsActive = false;
        user.IsMarkedAsDeleted = true;
    }
}