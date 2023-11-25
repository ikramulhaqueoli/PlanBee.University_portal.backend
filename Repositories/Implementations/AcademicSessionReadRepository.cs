using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.AcademicSessionDomain;

namespace PlanBee.University_portal.backend.Repositories.Implementations
{
    public class AcademicSessionReadRepository : IAcademicSessionReadRepository
    {
        private readonly IMongoReadRepository _repository;

        public AcademicSessionReadRepository(IMongoReadRepository repository)
        {
            _repository = repository;
        }

        public Task<List<AcademicSession>> GetAllAsync(bool activeOnly)
        {
            var filter = activeOnly 
                ? Builders<AcademicSession>.Filter.Eq(session => session.IsActive, activeOnly)
                : Builders<AcademicSession>.Filter.Empty;

            return _repository.GetManyAsync(filter);
        }
    }
}
