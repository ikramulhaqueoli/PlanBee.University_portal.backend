namespace PlanBee.University_portal.backend.Domain.Entities.DepartmentDomain
{
    public interface IDepartmentWriteRepository
    {
        Task SaveAsync(Department department);
    }
}
