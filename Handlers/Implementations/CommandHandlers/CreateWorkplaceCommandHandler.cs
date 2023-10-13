using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.DepartmentDomain;
using PlanBee.University_portal.backend.Domain.Entities.WorkplaceDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers;

public class CreateWorkplaceCommandHandler : AbstractCommandHandler<CreateWorkplaceCommand>
{
    private readonly IWorkplaceWriteRepository _workplaceWriteRepository;
    private readonly IDepartmentWriteRepository _departmentWriteRepository;

    public CreateWorkplaceCommandHandler(
        ILogger<CreateWorkplaceCommandHandler> logger,
        IWorkplaceWriteRepository workplaceWriteRepository,
        IDepartmentWriteRepository departmentWriteRepository)
        : base(logger)
    {
        _workplaceWriteRepository = workplaceWriteRepository;
        _departmentWriteRepository = departmentWriteRepository;
    }

    public override async Task<CommandResponse> HandleAsync(CreateWorkplaceCommand command)
    {
        var workplace = new Workplace
        {
            TitleAcronym = command.WorkplaceAcronym,
            Title = command.WorkplaceTitle,
            WorkplaceType = command.WorkplaceType
        };
        workplace.InitiateEntityBase();

        await _workplaceWriteRepository.SaveAsync(workplace);

        if (workplace.WorkplaceType == WorkplaceType.Academic)
        {
            await CreateNewDepartment(workplace.ItemId);
        }

        return new CommandResponse();
    }

    private Task CreateNewDepartment(string workplaceId)
    {
        var department = new Department
        {
            WorkplaceId = workplaceId,
        };
        department.InitiateEntityBase();

        return _departmentWriteRepository.SaveAsync(department);
    }
}