using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDesignationDomain;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations
{
    public class EmployeeDesignationRepository :
        IEmployeeDesignationReadRepository,
        IEmployeeDesignationWriteRepository
    {
        private readonly IMongoWriteRepository _mongoWriteRepository;
        private readonly IMongoReadRepository _mongoReadRepository;
        private readonly IEmployeeReadRepository _employeeReadRepository;

        public EmployeeDesignationRepository(
            IMongoWriteRepository mongoWriteRepository,
            IMongoReadRepository mongoReadRepository,
            IEmployeeReadRepository employeeReadRepository)
        {
            _mongoWriteRepository = mongoWriteRepository;
            _mongoReadRepository = mongoReadRepository;
            _employeeReadRepository = employeeReadRepository;
        }

        public Task<EmployeeDesignation?> Get(string itemId)
        {
            var filter = Builders<EmployeeDesignation>.Filter.Eq(nameof(EmployeeDesignation.ItemId), itemId);
            return _mongoReadRepository.GetFirstOrDefaultAsync(filter);
        }

        public Task<List<EmployeeDesignation>> GetActivesAsync(FilterDefinition<EmployeeDesignation>? customeFilter)
        {
            var filter = Builders<EmployeeDesignation>.Filter
                .Eq(nameof(EmployeeDesignation.IsActive), true);

            if (customeFilter != null) filter &= customeFilter;
            return _mongoReadRepository.GetAsync(filter);
        }

        public async Task<EmployeeDesignation?> GetDesignationByUserId(string baseUserId)
        {
            var employee = await _employeeReadRepository.GetByUserIdAsync(baseUserId);
            if (employee == null) return null;

            var filter = Builders<EmployeeDesignation>.Filter.Eq(nameof(EmployeeDesignation.ItemId), employee.DesignationId);
            return await _mongoReadRepository.GetFirstOrDefaultAsync(filter);
        }

        public Task SaveAsync(EmployeeDesignation designation)
        {
            return _mongoWriteRepository.SaveAsync(designation);
        }
    }
}
