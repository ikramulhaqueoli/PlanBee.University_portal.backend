namespace PlanBee.University_portal.backend.Domain.Entities.EmployeeDesignationDomain
{
    public interface IEmployeeDesignationReadRepository
    {
        Task<EmployeeDesignation?> GetAsync(string itemId);

        Task<List<EmployeeDesignation>> GetManyAsync(
            List<string>? specificItemIds = null,
            bool activeOnly = false);

        Task<EmployeeDesignation?> GetDesignationByUserIdAsync(string baseUserId);
    }
}
