using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.StudentDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations
{
    public class StudentRepository : IStudentReadRepository, IStudentWriteRepository
    {
        private readonly IMongoReadRepository _mongoReadRepository;
        private readonly IMongoWriteRepository _mongoWriteRepository;

        public StudentRepository(IMongoReadRepository mongoReadRepository,
            IMongoWriteRepository mongoWriteRepository)
        {
            _mongoReadRepository = mongoReadRepository;
            _mongoWriteRepository = mongoWriteRepository;
        }

        public Task<Student?> GetAsync(string itemId)
        {
            var filter = Builders<Student>.Filter.Eq(nameof(Student.ItemId), itemId);
            return _mongoReadRepository.GetFirstOrDefaultAsync(filter);
        }

        public Task<List<Student>> GetByDepartmentId(string departmentId)
        {
            var filter = Builders<Student>.Filter.Eq(nameof(Student.DepartmentId), departmentId);
            return _mongoReadRepository.GetManyAsync(filter);
        }

        public Task<Student?> GetByUserIdAsync(string baseUserId)
        {
            var filter = Builders<Student>.Filter.Eq(nameof(Student.BaseUserId), baseUserId);
            return _mongoReadRepository.GetFirstOrDefaultAsync(filter);
        }

        public Task SaveAsync(Student student)
        {
            return _mongoWriteRepository.SaveAsync(student);
        }
    }
}
