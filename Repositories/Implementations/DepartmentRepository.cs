using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.DepartmentDomain;
using PlanBee.University_portal.backend.Domain.Entities.WorkplaceDomain;

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

        public Task SaveAsync(Department department)
        {
            return _mongoWriteRepository.SaveAsync(department);
        }
    }
}
