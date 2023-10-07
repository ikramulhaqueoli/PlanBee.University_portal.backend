using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.WorkplaceDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations
{
    public class WorkplaceRepository
        : IWorkplaceReadRepository, IWorkplaceWriteRepository
    {
        private readonly IMongoReadRepository _mongoReadRepository;
        private readonly IMongoWriteRepository _mongoWriteRepository;

        public WorkplaceRepository(
            IMongoReadRepository mongoReadRepository,
            IMongoWriteRepository mongoWriteRepository)
        {
            _mongoReadRepository = mongoReadRepository;
            _mongoWriteRepository = mongoWriteRepository;
        }

        public Task<List<Workplace>> GetActiveAsync()
        {
            var filter = Builders<Workplace>.Filter.Eq(nameof(Workplace.IsActive), true);
            return _mongoReadRepository.GetAsync<Workplace>(filter);
        }

        public Task SaveAsync(Workplace workplace)
        {
            return _mongoWriteRepository.SaveAsync(workplace);
        }
    }
}
