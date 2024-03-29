using Microsoft.AspNetCore.Mvc;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Commands.Administrative;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Handlers;
using PlanBee.University_portal.backend.Start.Attributes;

namespace PlanBee.University_portal.backend.Start.Controllers;

[ApiController]
[Route("[Controller]")]
public class UniAdminController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public UniAdminController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [SuperAdmin]
    [HttpPost(template: "Session/Create")]
    public async Task<IActionResult> Create([FromBody] CreateAcademicSessionCommand command)
    {
        var response = await _commandDispatcher.DispatchAsync(command);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }

    [SuperAdmin]
    [HttpGet(template: "Session/All")]
    public async Task<IActionResult> GetManyAsync([FromQuery] GetAllAcademicSessionQuery query)
    {
        var response = await _queryDispatcher.DispatchAsync(query);

        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }
}
