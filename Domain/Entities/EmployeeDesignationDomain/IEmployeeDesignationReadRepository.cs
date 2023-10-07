namespace PlanBee.University_portal.backend.Domain.Entities.EmployeeDesignationDomain
{
    public interface IEmployeeDesignationReadRepository
    {
        public Task<List<EmployeeDesignation>> GetActiveAsync();
    }
}
