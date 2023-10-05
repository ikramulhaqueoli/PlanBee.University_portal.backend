using PlanBee.University_portal.backend.Domain.Constants;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.UniTemplateDomain;
using PlanBee.University_portal.backend.Domain.Entities.UserVerificationDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Utils;
using PlanBee.University_portal.backend.Infrastructure;

namespace PlanBee.University_portal.backend.Services.Implementations
{
    public class UniversityEmailService : IUniversityEmailService
    {
        private readonly IBaseUserReadRepository _userReadRepository;
        private readonly IBaseUserWriteRepository _userWriteRepository;
        private readonly IEmailSender _emailSender;
        private readonly IUniTemplateReadRepository _uniTemplateReadRepository;
        private readonly IUserVerificationWriteRepository _userVerificationWriteRepository;
        private readonly IUserVerificationReadRepository _userVerificationReadRepository;

        public UniversityEmailService(
            IBaseUserReadRepository userReadRepository,
            IEmailSender emailSender,
            IBaseUserWriteRepository userWriteRepository,
            IUniTemplateReadRepository uniTemplateReadRepository,
            IUserVerificationWriteRepository userVerificationWriteRepository,
            IUserVerificationReadRepository userVerificationReadRepository)
        {
            _userReadRepository = userReadRepository;
            _emailSender = emailSender;
            _userWriteRepository = userWriteRepository;
            _uniTemplateReadRepository = uniTemplateReadRepository;
            _userVerificationWriteRepository = userVerificationWriteRepository;
            _userVerificationReadRepository = userVerificationReadRepository;
        }

        public async Task SendSignupVerificationAsync(string baseUserId)
        {
            var baseUser = await _userReadRepository.GetAsync(baseUserId);
            if (baseUser == null) throw new ArgumentNullException(nameof(baseUser));

            var template = await _uniTemplateReadRepository.GetByKeyAsync(UniTemplateKeys.SignupVerificationMail);
            if (template == null) throw new ItemNotFoundException($"{nameof(UniTemplate)} with key {UniTemplateKeys.SignupVerificationMail} not found in the database.");

            var verificationCode = await CreateNewUserVerificationCodeAsync(baseUserId, UserVerificationType.Signup);
            var verificationLink = UserVerificationUtils.GetVerificationLink(verificationCode);

            var placeHolderDictionary = GetSignupVerificationPlaceHolders(baseUser, verificationLink);
            template.ResolveTemplate(placeHolderDictionary);

            var subject = template.Subject!;
            var body = template.Body;

            var success = await _emailSender.SendEmailAsync(
                baseUser.DisplayName,
                baseUser.PersonalEmail,
                subject,
                body);

            if (success) baseUser.SetAsVerificationSent();
            else baseUser.SetAsVerificationSendFail();

            await _userWriteRepository.UpdateAsync(baseUser);
        }

        private async Task<string> CreateNewUserVerificationCodeAsync(
            string baseUserId,
            UserVerificationType verificationType)
        {
            await _userVerificationWriteRepository.DeleteAllAsync(baseUserId, verificationType);

            var verification = new UserVerification();
            verification.InitiateEntityBase();
            verification.VerificationType = verificationType;
            verification.VerificationMedia = UserVerificationMedia.Email;
            verification.SetValidityByDays(BusinessConstants.VERIFI_CODE_VALIDITY_DAYS);

            await _userVerificationWriteRepository.SaveAsync(verification);
            return verification.VerificationCode;
        }

        private Dictionary<string, string> GetSignupVerificationPlaceHolders(
            BaseUser baseUser,
            string verificationLink)
            => new Dictionary<string, string>
            {
                {"receiverDisplayName", baseUser.DisplayName},
                {"verificationLink", verificationLink},
                {"universityHelpEmail", AppConfigUtil.Config.Institute.HelpEmail},
                {"universityHelpPhone", AppConfigUtil.Config.Institute.HelpPhone},
                {"senderName", "dummy sender"},
                {"senderPosition", "dummy position"},
            };
    }
}
