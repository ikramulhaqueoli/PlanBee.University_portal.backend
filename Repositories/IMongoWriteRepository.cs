using PlanBee.University_portal.backend.Domain.Entities;

namespace PlanBee.University_portal.backend.Repositories
{
    public interface IMongoWriteRepository
    {
        public Task SaveAsync<T>(T item) where T : EntityBase;

        public Task UpdateAsync<T>(T item) where T : EntityBase;
    }
}
