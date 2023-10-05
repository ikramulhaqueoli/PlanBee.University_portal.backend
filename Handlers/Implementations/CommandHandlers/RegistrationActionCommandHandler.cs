using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Services;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers
{
    public class RegistrationActionCommandHandler : AbstractCommandHandler<RegistrationActionCommand>
    {
        private readonly IRegistrationRequestReadRepository _registrationRequestReadRepository;
        private readonly IRegistrationRequestWriteRepository _registrationRequestWriteRepository;
        private readonly IEmployeeSignupService _employeeSignupService;

        public RegistrationActionCommandHandler(
            ILogger<RegistrationActionCommandHandler> logger,
            IRegistrationRequestWriteRepository registrationRequestWriteRepository,
            IEmployeeSignupService employeeSignupService,
            IRegistrationRequestReadRepository registrationRequestReadRepository) : base(logger)
        {
            _registrationRequestWriteRepository = registrationRequestWriteRepository;
            _employeeSignupService = employeeSignupService;
            _registrationRequestReadRepository = registrationRequestReadRepository;
        }

        public override async Task<CommandResponse> HandleAsync(RegistrationActionCommand command)
        {
            var registrationRequest = await _registrationRequestReadRepository.GetAsync(command.RegistrationRequestId);
            if (command.ActionStatus == RegistrationActionStatus.Approved)
            {
                if (registrationRequest.UserType == UserType.Employee)
                {
                    await _employeeSignupService.ApproveSignupRequest(registrationRequest);
                }
            }
            else if (command.ActionStatus == RegistrationActionStatus.Pending)
            {

            }

            else if (command.ActionStatus == RegistrationActionStatus.Rejected)
            {

            }

            registrationRequest.Approve(command.ActionComment, "dummy_user_id");
            await _registrationRequestWriteRepository.UpdateAsync(registrationRequest);

            return new CommandResponse();
        }
    }
}
