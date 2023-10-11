namespace PlanBee.University_portal.backend.Domain.Entities.DesignationDomain
{
    public interface IDesignationReadRepository
    {
        Task<Designation?> GetAsync(string itemId);

        Task<List<Designation>> GetManyAsync(
            List<string>? specificItemIds = null,
            bool activeOnly = false);

        Task<Designation?> GetDesignationByUserIdAsync(string baseUserId);
    }
}
