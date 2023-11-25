namespace PlanBee.University_portal.backend.Domain.Entities.AcademicSessionDomain
{
    public interface IAcademicSessionReadRepository
    {
        Task<List<AcademicSession>> GetAllAsync(bool activeOnly);
    }
}
