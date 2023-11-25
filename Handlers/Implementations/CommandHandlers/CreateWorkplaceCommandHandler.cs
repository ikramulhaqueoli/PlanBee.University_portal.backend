using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.DepartmentDomain;
using PlanBee.University_portal.backend.Domain.Entities.WorkplaceDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Services;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers;

public class CreateWorkplaceCommandHandler : AbstractCommandHandler<CreateWorkplaceCommand>
{
    private readonly IWorkplaceWriteRepository _workplaceWriteRepository;
    private readonly IDepartmentWriteRepository _departmentWriteRepository;
    private readonly IJwtAuthenticationService _jwtAuthenticationService;

    public CreateWorkplaceCommandHandler(
        ILogger<CreateWorkplaceCommandHandler> logger,
        IWorkplaceWriteRepository workplaceWriteRepository,
        IDepartmentWriteRepository departmentWriteRepository,
        IJwtAuthenticationService jwtAuthenticationService)
        : base(logger)
    {
        _workplaceWriteRepository = workplaceWriteRepository;
        _departmentWriteRepository = departmentWriteRepository;
        _jwtAuthenticationService = jwtAuthenticationService;
    }

    public override async Task<CommandResponse> HandleAsync(CreateWorkplaceCommand command)
    {
        var workplace = new Workplace
        {
            TitleAcronym = command.WorkplaceAcronym,
            Title = command.WorkplaceTitle,
            WorkplaceType = command.WorkplaceType
        };

        var creatorTokenUser = _jwtAuthenticationService.GetAuthTokenUser();
        workplace.InitiateEntityBase(creatorTokenUser.BaseUserId);

        await _workplaceWriteRepository.SaveAsync(workplace);

        if (workplace.WorkplaceType == WorkplaceType.Academic)
        {
            await CreateNewDepartment(workplace.ItemId, creatorTokenUser.BaseUserId);
        }

        return new CommandResponse();
    }

    private Task CreateNewDepartment(string workplaceId, string creatorBaseUserId)
    {
        var department = new Department
        {
            WorkplaceId = workplaceId,
        };
        department.InitiateEntityBase(creatorBaseUserId);

        return _departmentWriteRepository.SaveAsync(department);
    }
}