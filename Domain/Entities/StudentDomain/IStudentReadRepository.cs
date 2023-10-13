namespace PlanBee.University_portal.backend.Domain.Entities.StudentDomain
{
    public interface IStudentReadRepository
    {
        Task<Student?> GetAsync(string itemId);

        Task<Student?> GetByUserIdAsync(string baseUserId);
    }
}
