using Microsoft.AspNetCore.Mvc;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Handlers;
using PlanBee.University_portal.backend.Start.Attributes;

namespace PlanBee.University_portal.backend.Start.Controllers;

[ApiController]
[Route("[Controller]")]
public class WorkplaceController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public WorkplaceController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [SuperAdmin]
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateWorkplaceCommand command)
    {
        var response = await _commandDispatcher.DispatchAsync(command);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }

    [SuperAdmin]
    [HttpPost("AddDesignation")]
    public async Task<IActionResult> Create(CreateDesignationCommand command)
    {
        var response = await _commandDispatcher.DispatchAsync(command);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }
}