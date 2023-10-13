using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.DesignationDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Services;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers;

public class EmployeeSignupRequestCommandHandler : AbstractCommandHandler<EmployeeSignupRequestCommand>
{
    private readonly IUserSignupService _userSignupService;
    private readonly IDesignationReadRepository _designationReadRepository;

    public EmployeeSignupRequestCommandHandler(
        ILogger<EmployeeSignupRequestCommandHandler> logger,
        IUserSignupService userSignupService,
        IDesignationReadRepository designationReadRepository)
        : base(logger)
    {
        _userSignupService = userSignupService;
        _designationReadRepository = designationReadRepository;
    }

    public override async Task<CommandResponse> HandleAsync(EmployeeSignupRequestCommand command)
    {
        var newEmpDesignation = await _designationReadRepository.GetAsync(command.DesignationId)
            ?? throw new ItemNotFoundException($"Designation with Id {command.DesignationId} not found in the database.");
        command.DesignationTitle ??= newEmpDesignation.Title;
        command.DesignationType ??= newEmpDesignation.DesignationType;

        await _userSignupService.RequestSignupAsync(command, UserType.Employee);

        return new CommandResponse();
    }
}