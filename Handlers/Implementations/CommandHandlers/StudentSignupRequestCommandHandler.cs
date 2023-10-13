using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Services;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers;

public class StudentSignupRequestCommandHandler : AbstractCommandHandler<StudentSignupRequestCommand>
{
    private readonly IUserSignupService _userSignupService;

    public StudentSignupRequestCommandHandler(
        ILogger<StudentSignupRequestCommandHandler> logger,
        IUserSignupService userSignupService)
        : base(logger)
    {
        _userSignupService = userSignupService;
    }

    public override async Task<CommandResponse> HandleAsync(StudentSignupRequestCommand command)
    {
        await _userSignupService.RequestSignupAsync(command, UserType.Student);

        return new CommandResponse();
    }
}