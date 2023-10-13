using Microsoft.AspNetCore.Mvc;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Handlers;
using PlanBee.University_portal.backend.Start.Attributes;

namespace PlanBee.University_portal.backend.Start.Controllers;

[ApiController]
[Route("[Controller]")]
public class RegistrationRequestController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public RegistrationRequestController(
        ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [SuperAdmin]
    [HttpPost(template: "Approve")]
    public async Task<IActionResult> ApproveAsync([FromBody] RegistrationActionCommand command)
    {
        var response = await _commandDispatcher.DispatchAsync(command);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }

    [SuperAdmin]
    [HttpGet(template: "Get")]
    public async Task<IActionResult> GetManyAsync([FromQuery] GetRegistrationRequestsQuery query)
    {
        var response = await _queryDispatcher.DispatchAsync(query);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }

    [SuperAdmin]
    [HttpGet("SignupFormData")]
    public async Task<IActionResult> GetSignupFormDataAsync([FromQuery] GetSignupFormDataQuery query)
    {
        var response = await _queryDispatcher.DispatchAsync(query);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }
}