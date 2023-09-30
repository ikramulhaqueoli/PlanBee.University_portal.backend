using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities;

namespace PlanBee.University_portal.backend.Repositories
{
    public interface IMongoReadRepository
    {
        public Task<T> GetFirstOrDefaultAsync<T>(FilterDefinition<T> filter, bool excludeMarkedAsDeleted = true) where T : EntityBase;
    }
}
