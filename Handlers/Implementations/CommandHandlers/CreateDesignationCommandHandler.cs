using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers;

public class CreateDesignationCommandHandler : AbstractCommandHandler<CreateDesignationCommand>
{
    public CreateDesignationCommandHandler(ILogger<CreateDesignationCommandHandler> logger)
        : base(logger)
    {
    }

    public override async Task<CommandResponse> HandleAsync(CreateDesignationCommand command)
    {
        return new CommandResponse();
    }
}