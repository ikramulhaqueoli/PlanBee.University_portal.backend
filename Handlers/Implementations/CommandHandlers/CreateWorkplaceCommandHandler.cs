using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers;

public class CreateWorkplaceCommandHandler : AbstractCommandHandler<CreateWorkplaceCommand>
{
    public CreateWorkplaceCommandHandler(ILogger<CreateWorkplaceCommandHandler> logger)
        : base(logger)
    {
    }

    public override async Task<CommandResponse> HandleAsync(CreateWorkplaceCommand command)
    {
        return new CommandResponse();
    }
}