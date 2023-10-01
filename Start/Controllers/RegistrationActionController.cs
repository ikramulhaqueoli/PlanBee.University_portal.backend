using Microsoft.AspNetCore.Mvc;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Handlers;
using PlanBee.University_portal.backend.Start.Attributes;

namespace PlanBee.University_portal.backend.Start.Controllers;

[ApiController]
[Route("[Controller]")]
public class RegistrationActionController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public RegistrationActionController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [SuperAdmin]
    [HttpPost(template: "Approve")]
    public async Task<IActionResult> Approve(RegistrationActionCommand command)
    {
        var response = await _commandDispatcher.DispatchAsync(command);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }
}