using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Services;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers;

public class UserSignupCommandHandler : AbstractCommandHandler<UserSignupCommand>
{
    private readonly IUserSignupService _userSignupService;

    public UserSignupCommandHandler(
        ILogger<UserSignupCommandHandler> logger,
        IUserSignupService userSignupService)
        : base(logger)
    {
        _userSignupService = userSignupService;
    }

    public override async Task<CommandResponse> HandleAsync(UserSignupCommand command)
    {
        await _userSignupService.SignupAsync(command);

        return new CommandResponse();
    }
}