using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.UniTemplateDomain;
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

        public UniversityEmailService(
            IBaseUserReadRepository userReadRepository,
            IEmailSender emailSender,
            IBaseUserWriteRepository userWriteRepository,
            IUniTemplateReadRepository uniTemplateReadRepository)
        {
            _userReadRepository = userReadRepository;
            _emailSender = emailSender;
            _userWriteRepository = userWriteRepository;
            _uniTemplateReadRepository = uniTemplateReadRepository;
        }

        public async Task SendSignupVefificationAsync(string baseUserId)
        {
            var baseUser = await _userReadRepository.GetAsync(baseUserId);
            if (baseUser == null) throw new ArgumentNullException(nameof(baseUser));

            var template = await _uniTemplateReadRepository.GetByKeyAsync(UniTemplateKeys.SignupVerificationMail);
            if (template == null) throw new ItemNotFoundException($"{nameof(UniTemplate)} with key {UniTemplateKeys.SignupVerificationMail} not found in the database.");



            var placeHolderDictionary = new Dictionary<string, string>
            {
                {"receiverDisplayName", baseUser.DisplayName},
                {"verificationLink", ""},
                {"universityHelpEmail", AppConfigUtil.Config.Institute.HelpEmail},
                {"universityHelpPhone", AppConfigUtil.Config.Institute.HelpPhone},
                {"senderName", ""},
                {"senderPosition", ""},
            };

            template.ResolveTemplate(placeHolderDictionary);

            var subject = template.Subject!;
            var body = template.Body;

            var success = await _emailSender.SendEmailAsync(
                baseUser.DisplayName,
                baseUser.Email,
                subject,
                body);

            if (success) baseUser.SetAsVerificationSent();
            else baseUser.SetAsVerificationSendFail();

            await _userWriteRepository.UpdateAsync(baseUser);
        }

        
    }
}
