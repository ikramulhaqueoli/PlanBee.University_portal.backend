namespace PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain
{
    public interface IEmployeeWriteRepository
    {
        public Task SaveAsync(Employee employee);
    }
}
