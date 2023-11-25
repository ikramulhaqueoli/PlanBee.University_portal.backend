namespace PlanBee.University_portal.backend.Domain.Entities.AcademicSessionDomain
{
    public interface IAcademicSessionWriteRepository
    {
        Task SaveAsync(AcademicSession academicSession);
    }
}
