using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDesignationDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations
{
    public class EmployeeDesignationRepository :
        IEmployeeDesignationReadRepository,
        IEmployeeDesignationWriteRepository
    {
        private readonly IMongoWriteRepository _mongoWriteRepository;
        private readonly IMongoReadRepository _mongoReadRepository;

        public EmployeeDesignationRepository(
            IMongoWriteRepository mongoWriteRepository,
            IMongoReadRepository mongoReadRepository)
        {
            _mongoWriteRepository = mongoWriteRepository;
            _mongoReadRepository = mongoReadRepository;
        }

        public Task<List<EmployeeDesignation>> GetActiveAsync()
        {
            var filter = Builders<EmployeeDesignation>.Filter
                .Eq(nameof(EmployeeDesignation.IsActive), true);
            return _mongoReadRepository.GetAsync(filter);
        }

        public Task SaveAsync(EmployeeDesignation designation)
        {
            return _mongoWriteRepository.SaveAsync(designation);
        }
    }
}
