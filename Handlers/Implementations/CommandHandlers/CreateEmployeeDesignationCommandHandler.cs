using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDesignationDomain;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers
{
    public class CreateEmployeeDesignationCommandHandler
        : AbstractCommandHandler<CreateEmployeeDesignationCommand>
    {
        private readonly IEmployeeDesignationWriteRepository _employeeDesignationWriteRepository;

        public CreateEmployeeDesignationCommandHandler(
            ILogger<CreateEmployeeDesignationCommandHandler> logger,
            IEmployeeDesignationWriteRepository employeeDesignationWriteRepository)
            : base(logger)
        {
            _employeeDesignationWriteRepository = employeeDesignationWriteRepository;
        }

        public override async Task<CommandResponse> HandleAsync(CreateEmployeeDesignationCommand command)
        {
            var designation = new EmployeeDesignation
            {
                Title = command.Title,
                DesignationType = command.DesignationType
            };
            designation.InitiateEntityBase();

            await _employeeDesignationWriteRepository.SaveAsync(designation);

            return new CommandResponse();
        }
    }
}
