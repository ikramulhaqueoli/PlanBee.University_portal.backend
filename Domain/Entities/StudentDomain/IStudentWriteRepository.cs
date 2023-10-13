using PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain;

namespace PlanBee.University_portal.backend.Domain.Entities.StudentDomain
{
    public interface IStudentWriteRepository
    {
        public Task SaveAsync(Student student);
    }
}
