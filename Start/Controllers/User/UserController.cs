using Microsoft.AspNetCore.Mvc;
using PlanBee.University_portal.backend.CommandHandlers;
using PlanBee.University_portal.backend.Domain.Commands;

namespace PlanBee.University_portal.backend.Start.Controllers.User;

public class UserController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public UserController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost(template: "signup")]
    public async Task<IActionResult> Signup(UserSignupCommand command)
    {
        var response = await _commandDispatcher.DispatchAsync(command);
        
        return response.Success
            ? Ok(response) 
            : BadRequest(response);
    }
}