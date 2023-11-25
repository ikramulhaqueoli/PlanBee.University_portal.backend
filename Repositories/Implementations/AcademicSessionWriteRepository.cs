using PlanBee.University_portal.backend.Domain.Entities.AcademicSessionDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations
{
    public class AcademicSessionWriteRepository : IAcademicSessionWriteRepository
    {
        private readonly IMongoWriteRepository _repository;

        public AcademicSessionWriteRepository(IMongoWriteRepository repository)
        {
            _repository = repository;
        }

        public Task SaveAsync(AcademicSession academicSession)
        {
            return _repository.SaveAsync(academicSession);
        }
    }
}
