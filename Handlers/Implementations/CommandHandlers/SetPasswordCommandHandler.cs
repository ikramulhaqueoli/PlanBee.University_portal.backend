using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Services;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers
{
    public class SetPasswordCommandHandler : AbstractCommandHandler<SetPasswordCommand>
    {
        private readonly IUserVerificationService _userVerificationService;

        public SetPasswordCommandHandler(
            ILogger<SetPasswordCommandHandler> logger, 
            IUserVerificationService userVerificationService) 
            : base(logger)
        {
            _userVerificationService = userVerificationService;
        }

        public override async Task<CommandResponse> HandleAsync(SetPasswordCommand command)
        {
            var verified = await _userVerificationService.VerifyFromEmailAsync(command.VerificationCode, command.NewPassword);

            if (verified is false)
            {
                throw new GeneralBusinessException("Invalid Verification Attempt.");
            }

            return new CommandResponse();
        }
    }
}
