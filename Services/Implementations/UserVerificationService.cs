using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.UserVerificationDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Services.Implementations
{
    public class UserVerificationService : IUserVerificationService
    {
        private readonly IUserVerificationReadRepository _userVerificationReadRepository;
        private readonly IBaseUserReadRepository _baseUserReadRepository;
        private readonly IBaseUserWriteRepository _baseUserWriteRepository;
        private readonly IUserVerificationWriteRepository _userVerificationWriteRepository;

        public UserVerificationService(
            IUserVerificationReadRepository userVerificationReadRepository,
            IBaseUserReadRepository baseUserReadRepository,
            IBaseUserWriteRepository baseUserWriteRepository,
            IUserVerificationWriteRepository userVerificationWriteRepository)
        {
            _userVerificationReadRepository = userVerificationReadRepository;
            _baseUserReadRepository = baseUserReadRepository;
            _baseUserWriteRepository = baseUserWriteRepository;
            _userVerificationWriteRepository = userVerificationWriteRepository;
        }

        public async Task<bool> VerifyFromEmailAsync(string verificationCode, string password)
        {
            var verification = await _userVerificationReadRepository.GetByVerificationCodeAsync(verificationCode);
            if (verification == null ||
                verification.VerificationMedia != UserVerificationMedia.Email ||
                verification.IsStillValid() is false)
                    return false;

            var baseUser = await _baseUserReadRepository.GetAsync(verification.BaseUserId);
            if (baseUser == null) return false;

            if (verification.VerificationType == UserVerificationType.Signup)
            {
                baseUser.SetAsVerified();
                baseUser.SetPasswordAsHash(password);
            }

            await _baseUserWriteRepository.UpdateAsync(baseUser);

            await _userVerificationWriteRepository.DeleteAllAsync(
                verification.BaseUserId,
                verification.VerificationType);

            return true;
        }
    }
}
