using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Services;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers;

public class EmployeeSignupCommandHandler : AbstractCommandHandler<EmployeeSignupCommand>
{
    private readonly IEmployeeSignupService _employeeSignupService;

    public EmployeeSignupCommandHandler(
        ILogger<EmployeeSignupCommandHandler> logger,
        IEmployeeSignupService employeeSignupService)
        : base(logger)
    {
        _employeeSignupService = employeeSignupService;
    }

    public override async Task<CommandResponse> HandleAsync(EmployeeSignupCommand command)
    {
        await _employeeSignupService.SignupAsync(command);

        return new CommandResponse();
    }
}