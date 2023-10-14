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

        public Task<List<Workplace>> GetManyAsync(
            List<string>? specificItemIds = null,
            bool isActiveOnly = false)
        {
            var filter = Builders<Workplace>.Filter.Empty;

            if (specificItemIds != null)
            {
                filter &= Builders<Workplace>.Filter.In(nameof(Workplace.ItemId), specificItemIds);
            }

            if (isActiveOnly)
            {
                filter &= Builders<Workplace>.Filter.Eq(nameof(Workplace.IsActive), true);
            }
                
            return _mongoReadRepository.GetManyAsync(filter);
        }

        public Task SaveAsync(Workplace workplace)
        {
            return _mongoWriteRepository.SaveAsync(workplace);
        }
    }
}
