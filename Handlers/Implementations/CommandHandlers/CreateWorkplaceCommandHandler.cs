using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.WorkplaceDomain;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers;

public class CreateWorkplaceCommandHandler : AbstractCommandHandler<CreateWorkplaceCommand>
{
    private readonly IWorkplaceWriteRepository _workplaceWriteRepository;

    public CreateWorkplaceCommandHandler(
        ILogger<CreateWorkplaceCommandHandler> logger,
        IWorkplaceWriteRepository workplaceWriteRepository)
        : base(logger)
    {
        _workplaceWriteRepository = workplaceWriteRepository;
    }

    public override async Task<CommandResponse> HandleAsync(CreateWorkplaceCommand command)
    {
        var workplace = new Workplace
        {
            WorkplaceAcronym = command.WorkplaceAcronym,
            WorkplaceTitle = command.WorkplaceTitle,
            WorkplaceType = command.WorkplaceType
        };
        workplace.InitiateEntityBase();

        await _workplaceWriteRepository.SaveAsync(workplace);
        return new CommandResponse();
    }
}