using PlanBee.University_portal.backend.Domain.Constants;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain;
using PlanBee.University_portal.backend.Domain.Entities.UniTemplateDomain;
using PlanBee.University_portal.backend.Domain.Entities.UserVerificationDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Models;
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
        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        private readonly IEmployeeReadRepository _employeeReadRepository;

        public UniversityEmailService(
            IBaseUserReadRepository userReadRepository,
            IEmailSender emailSender,
            IBaseUserWriteRepository userWriteRepository,
            IUniTemplateReadRepository uniTemplateReadRepository,
            IUserVerificationWriteRepository userVerificationWriteRepository,
            IUserVerificationReadRepository userVerificationReadRepository,
            IJwtAuthenticationService jwtAuthenticationService,
            IEmployeeReadRepository employeeReadRepository)
        {
            _userReadRepository = userReadRepository;
            _emailSender = emailSender;
            _userWriteRepository = userWriteRepository;
            _uniTemplateReadRepository = uniTemplateReadRepository;
            _userVerificationWriteRepository = userVerificationWriteRepository;
            _userVerificationReadRepository = userVerificationReadRepository;
            _jwtAuthenticationService = jwtAuthenticationService;
            _employeeReadRepository = employeeReadRepository;
        }

        public async Task SendSignupVerificationAsync(
            AuthTokenUser fromTokenUser,
            BaseUser toBaseUser,
            string senderDesignation)
        {
            var templateKey = GetSignupVerifyTemplateKey(toBaseUser);

            var template = await _uniTemplateReadRepository.GetByKeyAsync(templateKey) ?? throw new ItemNotFoundException($"{nameof(UniTemplate)} with key {templateKey} not found in the database.");
            var verificationCode = await CreateNewUserVerificationCodeAsync(toBaseUser.ItemId, UserVerificationType.Signup);
            var verificationLink = UserVerificationUtils.GetVerificationLink(verificationCode);

            var placeHolderDictionary = GetSignupVerificationPlaceHolders(
                fromTokenUser,
                toBaseUser,
                verificationLink,
                senderDesignation);

            template.ResolveTemplate(placeHolderDictionary);

            var subject = template.Subject!;
            var body = template.Body;

            var success = await _emailSender.SendEmailAsync(
                toBaseUser.GetDisplayName(),
                toBaseUser.PersonalEmail,
                subject,
                body);

            if (success) toBaseUser.SetAsVerificationSent();
            else toBaseUser.SetAsVerificationSendFail();

            await _userWriteRepository.UpdateAsync(toBaseUser);
        }

        private async Task<string> CreateNewUserVerificationCodeAsync(
            string baseUserId,
            UserVerificationType verificationType)
        {
            await _userVerificationWriteRepository.DeleteAllAsync(baseUserId, verificationType);

            var verification = new UserVerification();
            verification.Initiate(verificationType, baseUserId);

            await _userVerificationWriteRepository.SaveAsync(verification);
            return verification.VerificationCode;
        }

        private Dictionary<string, string> GetSignupVerificationPlaceHolders(
            AuthTokenUser fromTokenUser,
            BaseUser toBaseUser,
            string senderDesignation,
            string verificationLink)
        {

            return new()
            {
                {"receiverDisplayName", toBaseUser.GetDisplayName()},
                {"verificationLink", verificationLink},
                {"universityHelpEmail", AppConfigUtil.Config.Institute.HelpEmail},
                {"universityHelpPhone", AppConfigUtil.Config.Institute.HelpPhone},
                {"senderName", fromTokenUser.DisplayName},
                {"senderPosition", senderDesignation},
            };
        }

        private string GetSignupVerifyTemplateKey(BaseUser toBaseUser)
            => toBaseUser.UserType switch
            {
                UserType.Employee => UniTemplateKeys.SignupVerifyMailTeacher,
                UserType.Student => UniTemplateKeys.SignupVerifyMailStudent,
                _ => throw new GeneralBusinessException($"Signup Verification Tempate for UserType: {toBaseUser.UserType} not registered in the system.")
            };
    }
}
