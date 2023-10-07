namespace PlanBee.University_portal.backend.Domain.Entities.WorkplaceDomain
{
    public interface IWorkplaceReadRepository
    {
        Task<List<Workplace>> GetActiveAsync();
    }
}
