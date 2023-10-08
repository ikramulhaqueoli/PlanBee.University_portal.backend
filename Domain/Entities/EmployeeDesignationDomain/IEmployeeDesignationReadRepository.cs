using MongoDB.Driver;

namespace PlanBee.University_portal.backend.Domain.Entities.EmployeeDesignationDomain
{
    public interface IEmployeeDesignationReadRepository
    {
        Task<EmployeeDesignation?> Get(string itemId);

        Task<List<EmployeeDesignation>> GetActivesAsync(FilterDefinition<EmployeeDesignation>? customeFilter = null);

        Task<EmployeeDesignation?> GetDesignationByUserId(string baseUserId);
    }
}
