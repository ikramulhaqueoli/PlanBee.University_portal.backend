using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands.Administrative;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Services;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers.Administrative
{
    internal class CreateAcademicSessionCommandHandler : AbstractCommandHandler<CreateAcademicSessionCommand>
    {
        private readonly IUniAdministrativeService _uniAdministrativeService;

        public CreateAcademicSessionCommandHandler(
            ILogger<CreateAcademicSessionCommandHandler> logger,
            IUniAdministrativeService uniAdministrativeService)
            : base(logger)
        {
            _uniAdministrativeService = uniAdministrativeService;
        }

        public override async Task<CommandResponse> HandleAsync(CreateAcademicSessionCommand command)
        {
            await _uniAdministrativeService.AddSessionAsync(command);
            return new CommandResponse();
        }
    }
}
