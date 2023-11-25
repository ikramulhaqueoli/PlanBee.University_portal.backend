using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.DesignationDomain;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Services;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers
{
    public class CreateDesignationCommandHandler
        : AbstractCommandHandler<CreateDesignationCommand>
    {
        private readonly IDesignationWriteRepository _designationWriteRepository;
        private readonly IJwtAuthenticationService _jwtAuthenticationService;

        public CreateDesignationCommandHandler(
            ILogger<CreateDesignationCommandHandler> logger,
            IDesignationWriteRepository designationWriteRepository,
            IJwtAuthenticationService jwtAuthenticationService)
            : base(logger)
        {
            _designationWriteRepository = designationWriteRepository;
            _jwtAuthenticationService = jwtAuthenticationService;
        }

        public override async Task<CommandResponse> HandleAsync(CreateDesignationCommand command)
        {
            var designation = new Designation
            {
                Title = command.Title,
                DesignationType = command.DesignationType
            };

            var creatorTokenUser = _jwtAuthenticationService.GetAuthTokenUser();
            designation.InitiateEntityBase(creatorTokenUser.BaseUserId);

            await _designationWriteRepository.SaveAsync(designation);

            return new CommandResponse();
        }
    }
}
