using Microsoft.AspNetCore.Mvc;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Handlers;
using PlanBee.University_portal.backend.Start.Attributes;

namespace PlanBee.University_portal.backend.Start.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserVerificationController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public UserVerificationController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet(template: "CodeValidity")]
    public async Task<IActionResult> All([FromQuery] VerificationCodeValidityQuery query)
    {
        var response = await _queryDispatcher.DispatchAsync(query);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpPost(template: "VerifySetPassword")]
    public async Task<IActionResult> SetPasswordOnVerified([FromBody] VerifySetPasswordCommand command)
    {
        var response = await _commandDispatcher.DispatchAsync(command);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }
}