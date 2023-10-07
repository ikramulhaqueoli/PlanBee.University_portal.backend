namespace PlanBee.University_portal.backend.Domain.Entities.WorkplaceDomain
{
    public interface IWorkplaceWriteRepository
    {
        Task SaveAsync(Workplace workplace);
    }
}
