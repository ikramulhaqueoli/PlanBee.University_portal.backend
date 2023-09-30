using Microsoft.AspNetCore.Mvc;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Handlers;

namespace PlanBee.University_portal.backend.Start.Controllers;

[ApiController]
[Route("[Controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public EmployeeController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost(template: "Create")]
    public async Task<IActionResult> Create([FromBody] EmployeeSignupCommand command)
    {
        var response = await _commandDispatcher.DispatchAsync(command);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpGet(template: "All")]
    public async Task<IActionResult> All([FromQuery] GetAllEmployeeQuery query)
    {
        var response = await _queryDispatcher.DispatchAsync(query);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }
}