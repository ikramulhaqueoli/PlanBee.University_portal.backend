using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeReadRepository, IEmployeeWriteRepository
    {
        private IMongoReadRepository _mongoReadRepository;
        private IMongoWriteRepository _mongoWriteRepository;

        public EmployeeRepository(IMongoReadRepository mongoReadRepository, IMongoWriteRepository mongoWriteRepository)
        {
            _mongoReadRepository = mongoReadRepository;
            _mongoWriteRepository = mongoWriteRepository;
        }

        public Task<Employee?> GetAsync(string itemId)
        {
            var filter = Builders<Employee>.Filter.Eq(nameof(Employee.ItemId), itemId);
            return _mongoReadRepository.GetFirstOrDefaultAsync(filter);
        }

        public Task<Employee?> GetByUserIdAsync(string baseUserId)
        {
            var filter = Builders<Employee>.Filter.Eq(nameof(Employee.BaseUserId), baseUserId);
            return _mongoReadRepository.GetFirstOrDefaultAsync(filter);
        }

        public Task SaveAsync(Employee employee)
        {
            return _mongoWriteRepository.SaveAsync(employee);
        }
    }
}
