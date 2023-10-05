using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Enums.System;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Services;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers
{
    public class VerifySignupEmailCommandHandler : AbstractCommandHandler<VerifySignupEmailCommand>
    {
        private readonly IUserVerificationService _userVerificationService;

        public VerifySignupEmailCommandHandler(
            ILogger<VerifySignupEmailCommandHandler> logger, 
            IUserVerificationService userVerificationService) 
            : base(logger)
        {
            _userVerificationService = userVerificationService;
        }

        public override async Task<CommandResponse> HandleAsync(VerifySignupEmailCommand command)
        {
            var verified = await _userVerificationService.VerifyFromEmailAsync(command.VerificationCode, command.NewPassword);

            var response = new CommandResponse();
            if (verified is false)
            {
                response.SetCommandError(ResponseErrorType.BusinessException, "Invalid Verification Attempt.");
            }

            return response;
        }
    }
}
