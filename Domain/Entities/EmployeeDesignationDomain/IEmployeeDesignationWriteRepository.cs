namespace PlanBee.University_portal.backend.Domain.Entities.EmployeeDesignationDomain
{
    public interface IEmployeeDesignationWriteRepository
    {
        public Task SaveAsync(EmployeeDesignation designation);
    }
}
