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

        public Task<Employee> GetAsync(string itemId)
        {
            return _mongoReadRepository.GetFirstOrDefaultAsync<Employee>(itemId);
        }

        public Task SaveAsync(Employee employee)
        {
            return _mongoWriteRepository.SaveAsync(employee);
        }
    }
}
