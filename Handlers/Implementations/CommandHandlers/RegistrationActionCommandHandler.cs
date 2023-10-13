using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Services;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers
{
    public class RegistrationActionCommandHandler : AbstractCommandHandler<RegistrationActionCommand>
    {
        private readonly IRegistrationRequestReadRepository _registrationRequestReadRepository;
        private readonly IRegistrationRequestWriteRepository _registrationRequestWriteRepository;
        private readonly IUserSignupService _userSignupService;
        private readonly IJwtAuthenticationService _jwtAuthenticationService;

        public RegistrationActionCommandHandler(
            ILogger<RegistrationActionCommandHandler> logger,
            IRegistrationRequestWriteRepository registrationRequestWriteRepository,
            IUserSignupService userSignupService,
            IRegistrationRequestReadRepository registrationRequestReadRepository,
            IJwtAuthenticationService jwtAuthenticationService) : base(logger)
        {
            _registrationRequestWriteRepository = registrationRequestWriteRepository;
            _userSignupService = userSignupService;
            _registrationRequestReadRepository = registrationRequestReadRepository;
            _jwtAuthenticationService = jwtAuthenticationService;
        }

        public override async Task<CommandResponse> HandleAsync(RegistrationActionCommand command)
        {
            var tokenUser = _jwtAuthenticationService.GetAuthTokenUser();

            var registrationRequest = await _registrationRequestReadRepository.GetPendingAsync(command.RegistrationRequestId);
            if (registrationRequest == null)
            {
                throw new ItemNotFoundException($"RegistrationRequest with Id {command.RegistrationRequestId} is not Pending in the database.");
            }

            if (command.ActionStatus == RegistrationActionStatus.Approved)
            {
                await ApproveRequestAsync(registrationRequest);
                registrationRequest.Approve(command.ActionComment, tokenUser.BaseUserId);
            }
            else if (command.ActionStatus == RegistrationActionStatus.Pending)
            {
                registrationRequest.KeepPending(command.ActionComment, tokenUser.BaseUserId);
            }
            else if (command.ActionStatus == RegistrationActionStatus.Rejected)
            {
                registrationRequest.Reject(command.ActionComment, tokenUser.BaseUserId);
            }

            await _registrationRequestWriteRepository.UpdateAsync(registrationRequest);

            return new CommandResponse();
        }

        private async Task ApproveRequestAsync(RegistrationRequest registrationRequest)
        {
            await _userSignupService.ApproveSignupRequest(registrationRequest);
        }
    }
}
