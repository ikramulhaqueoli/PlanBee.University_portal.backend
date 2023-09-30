using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;
using PlanBee.University_portal.backend.Domain.Enums;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Services;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers
{
    public class RegistrationActionCommandHandler : AbstractCommandHandler<RegistrationActionCommand>
    {
        private readonly ILogger<RegistrationActionCommandHandler> _logger;
        private readonly IRegistrationRequestReadRepository _registrationRequestReadRepository;
        private readonly IRegistrationRequestWriteRepository _registrationRequestWriteRepository;
        private readonly IEmployeeSignupService _employeeSignupService;

        public RegistrationActionCommandHandler(
            ILogger<RegistrationActionCommandHandler> logger,
            IRegistrationRequestWriteRepository registrationRequestWriteRepository,
            IEmployeeSignupService employeeSignupService,
            IRegistrationRequestReadRepository registrationRequestReadRepository) : base(logger)
        {
            _logger = logger;
            _registrationRequestWriteRepository = registrationRequestWriteRepository;
            _employeeSignupService = employeeSignupService;
            _registrationRequestReadRepository = registrationRequestReadRepository;
        }

        public override async Task<CommandResponse> HandleAsync(RegistrationActionCommand command)
        {
            var registrationRequest = await _registrationRequestReadRepository.GetAsync(command.RegistrationRequestId);
            if (command.ActionStatus == RequestActionStatus.Approved)
            {
                if (registrationRequest.UserType == UserType.Employee)
                {
                    await _employeeSignupService.ApproveSignupRequest(registrationRequest);
                }
            }
            else if (command.ActionStatus == RequestActionStatus.Waiting)
            {

            }

            else if (command.ActionStatus == RequestActionStatus.Rejected)
            {

            }

            registrationRequest.Approve(command.ActionComment, "dummy_user_id");
            await _registrationRequestWriteRepository.UpdateAsync(registrationRequest);

            return new CommandResponse();
        }
    }
}
