using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.DepartmentDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations
{
    public class DepartmentRepository : IDepartmentReadRepository, IDepartmentWriteRepository
    {
        private readonly IMongoReadRepository _mongoReadRepository;
        private readonly IMongoWriteRepository _mongoWriteRepository;

        public DepartmentRepository(
            IMongoReadRepository mongoReadRepository,
            IMongoWriteRepository mongoWriteRepository)
        {
            _mongoReadRepository = mongoReadRepository;
            _mongoWriteRepository = mongoWriteRepository;
        }

        public Task<Department?> GetAsync(string itemId)
        {
            var filter = Builders<Department>.Filter.Eq(nameof(Department.ItemId), itemId);
            return _mongoReadRepository.GetFirstOrDefaultAsync(filter);
        }

        public Task<List<Department>> GetManyAsync(
            List<string>? specificItemIds = null,
            bool isActiveOnly = false)
        {
            var filter = Builders<Department>.Filter.Empty;

            if (specificItemIds != null)
            {
                filter &= Builders<Department>.Filter.In(nameof(Department.ItemId), specificItemIds);
            }

            if (isActiveOnly)
            {
                filter &= Builders<Department>.Filter.Eq(nameof(Department.IsActive), true);
            }

            return _mongoReadRepository.GetAsync(filter);
        }

        public Task SaveAsync(Department department)
        {
            return _mongoWriteRepository.SaveAsync(department);
        }
    }
}
