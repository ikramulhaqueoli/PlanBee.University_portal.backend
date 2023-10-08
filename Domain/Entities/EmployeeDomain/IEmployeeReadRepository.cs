namespace PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain
{
    public interface IEmployeeReadRepository
    {
        Task<Employee?> GetAsync(string itemId);
        
        Task<Employee?> GetByUserIdAsync(string baseUserId);

        Task<List<Employee>> GetAllAsync();
    }
}
