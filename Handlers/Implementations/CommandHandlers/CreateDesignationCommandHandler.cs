using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.DesignationDomain;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers
{
    public class CreateDesignationCommandHandler
        : AbstractCommandHandler<CreateDesignationCommand>
    {
        private readonly IDesignationWriteRepository _designationWriteRepository;

        public CreateDesignationCommandHandler(
            ILogger<CreateDesignationCommandHandler> logger,
            IDesignationWriteRepository designationWriteRepository)
            : base(logger)
        {
            _designationWriteRepository = designationWriteRepository;
        }

        public override async Task<CommandResponse> HandleAsync(CreateDesignationCommand command)
        {
            var designation = new Designation
            {
                Title = command.Title,
                DesignationType = command.DesignationType
            };
            designation.InitiateEntityBase();

            await _designationWriteRepository.SaveAsync(designation);

            return new CommandResponse();
        }
    }
}
