namespace PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain
{
    public interface IEmployeeReadRepository
    {
        public Task<Employee> GetAsync(string itemId);
    }
}
