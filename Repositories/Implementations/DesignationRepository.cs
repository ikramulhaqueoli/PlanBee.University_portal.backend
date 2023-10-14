using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.DesignationDomain;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations
{
    public class DesignationRepository :
        IDesignationReadRepository,
        IDesignationWriteRepository
    {
        private readonly IMongoWriteRepository _mongoWriteRepository;
        private readonly IMongoReadRepository _mongoReadRepository;
        private readonly IEmployeeReadRepository _employeeReadRepository;

        public DesignationRepository(
            IMongoWriteRepository mongoWriteRepository,
            IMongoReadRepository mongoReadRepository,
            IEmployeeReadRepository employeeReadRepository)
        {
            _mongoWriteRepository = mongoWriteRepository;
            _mongoReadRepository = mongoReadRepository;
            _employeeReadRepository = employeeReadRepository;
        }

        public Task<Designation?> GetAsync(string itemId)
        {
            var filter = Builders<Designation>.Filter.Eq(nameof(Designation.ItemId), itemId);
            return _mongoReadRepository.GetFirstOrDefaultAsync(filter);
        }

        public Task<List<Designation>> GetManyAsync(
            List<string>? specificItemIds = null,
            bool activeOnly = false)
        {
            var filter = Builders<Designation>.Filter.Empty;
            if (activeOnly)
            {
                filter &= Builders<Designation>.Filter
                    .Eq(nameof(Designation.IsActive), true);
            }
            
            if (specificItemIds != null)
            {
                filter &= Builders<Designation>.Filter
                    .In(nameof(Designation.ItemId), specificItemIds);
            }

            return _mongoReadRepository.GetManyAsync(filter);
        }

        public async Task<Designation?> GetDesignationByUserIdAsync(string baseUserId)
        {
            var employee = await _employeeReadRepository.GetByUserIdAsync(baseUserId);
            if (employee == null) return null;

            var filter = Builders<Designation>.Filter.Eq(nameof(Designation.ItemId), employee.DesignationId);
            return await _mongoReadRepository.GetFirstOrDefaultAsync(filter);
        }

        public Task SaveAsync(Designation designation)
        {
            return _mongoWriteRepository.SaveAsync(designation);
        }
    }
}
