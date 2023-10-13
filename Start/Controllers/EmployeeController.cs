using Microsoft.AspNetCore.Mvc;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Handlers;
using PlanBee.University_portal.backend.Start.Attributes;

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

    [SuperAdmin]
    [HttpPost(template: "Create")]
    public async Task<IActionResult> Create([FromBody] EmployeeSignupRequestCommand command)
    {
        var response = await _commandDispatcher.DispatchAsync(command);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }

    [SuperAdmin]
    [HttpGet(template: "Get")]
    public async Task<IActionResult> GetManyAsync([FromQuery] GetEmployeesQuery query)
    {
        var response = await _queryDispatcher.DispatchAsync(query);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }

    [SuperAdmin]
    [HttpPost("AddDesignation")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateDesignationCommand command)
    {
        var response = await _commandDispatcher.DispatchAsync(command);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }

    [SuperAdmin]
    [HttpGet("GetDesignations")]
    public async Task<IActionResult> GetDesignationsAsync([FromQuery] GetDesignationsQuery query)
    {
        var response = await _queryDispatcher.DispatchAsync(query);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }
}