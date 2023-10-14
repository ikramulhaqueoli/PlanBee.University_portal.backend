namespace PlanBee.University_portal.backend.Domain.Entities.DepartmentDomain
{
    public interface IDepartmentReadRepository
    {
        Task<Department?> GetAsync(string itemId);

        Task<Department?> GetByWorkplaceIdAsync(string workplaceId);

        Task<List<Department>> GetManyAsync(
            List<string>? specificItemIds = null,
            bool isActiveOnly = false);
    }
}
