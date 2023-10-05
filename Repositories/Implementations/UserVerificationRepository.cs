using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.UserVerificationDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Repositories.Implementations
{
    internal class UserVerificationRepository :
        IUserVerificationReadRepository,
        IUserVerificationWriteRepository
    {
        private readonly IMongoReadRepository _mongoReadRepository;
        private readonly IMongoWriteRepository _mongoWriteRepository;

        public UserVerificationRepository(
            IMongoReadRepository mongoReadRepository,
            IMongoWriteRepository mongoWriteRepository)
        {
            _mongoReadRepository = mongoReadRepository;
            _mongoWriteRepository = mongoWriteRepository;
        }

        public Task DeleteAllAsync(string baseUserId, UserVerificationType verificationType)
        {
            var filter = Builders<UserVerification>.Filter.Eq(nameof(UserVerification.BaseUserId), baseUserId) &
                         Builders<UserVerification>.Filter.Eq(nameof(UserVerification.VerificationType), verificationType);
            return _mongoWriteRepository.DeleteAsync(filter);
        }

        public async Task<UserVerification?> GetByVerificationCodeAsync(string verificationCode)
        {
            var filter = Builders<UserVerification>.Filter.Eq(nameof(UserVerification.VerificationCode), verificationCode);
            var results = await _mongoReadRepository.GetAsync(filter);

            return results?.OrderBy(r => r.CreatedOn).LastOrDefault();
        }

        public Task SaveAsync(UserVerification userVerification)
        {
            return _mongoWriteRepository.SaveAsync(userVerification);
        }
    }
}
