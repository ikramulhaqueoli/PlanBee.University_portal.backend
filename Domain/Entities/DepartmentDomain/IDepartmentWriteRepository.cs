using PlanBee.University_portal.backend.Domain.Entities.DepartmentDomain;

namespace PlanBee.University_portal.backend.Domain.Entities.WorkplaceDomain
{
    public interface IDepartmentWriteRepository
    {
        Task SaveAsync(Department department);
    }
}
