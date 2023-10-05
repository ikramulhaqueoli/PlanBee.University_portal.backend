using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.UniTemplateDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations
{
    public class UniTemplateRepository : IUniTemplateReadRepository
    {
        private readonly IMongoReadRepository _mongoReadRepository;

        public UniTemplateRepository(IMongoReadRepository mongoReadRepository)
        {
            _mongoReadRepository = mongoReadRepository;
        }

        public Task<UniTemplate?> GetByKeyAsync(string key)
        {
            var filter = Builders<UniTemplate>.Filter.Eq(nameof(UniTemplate.Key), key);
            return _mongoReadRepository.GetFirstOrDefaultAsync(filter);
        }
    }
}
