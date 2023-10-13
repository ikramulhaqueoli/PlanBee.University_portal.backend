namespace PlanBee.University_portal.backend.Domain.Entities.DepartmentDomain
{
    public interface IDepartmentReadRepository
    {
        Task<Department?> GetAsync(string itemId);
    }
}
