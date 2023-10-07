using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Services;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers;

public class StudentSignupCommandHandler : AbstractCommandHandler<StudentSignupCommand>
{
    private readonly IStudentSignupService _studentSignupService;

    public StudentSignupCommandHandler(
        ILogger<StudentSignupCommandHandler> logger,
        IStudentSignupService studentSignupService)
        : base(logger)
    {
        _studentSignupService = studentSignupService;
    }

    public override async Task<CommandResponse> HandleAsync(StudentSignupCommand command)
    {
        await _studentSignupService.SignupAsync(command);

        return new CommandResponse();
    }
}