using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

namespace PlanBee.University_portal.backend.Start.Controllers;

[ApiController]
public class HealthCheckController : ControllerBase
{
    private readonly IBaseUserWriteRepository _userWriteRepository;

    public HealthCheckController(IBaseUserWriteRepository userWriteRepository)
    {
        _userWriteRepository = userWriteRepository;
    }

    [HttpGet("healthcheck")]
    public ActionResult<string> Get()
    {
        return Ok("Healthy");
    }

    [HttpGet("healthcheck/anonymous")]
    [Authorize(Roles = "Anonymous")]
    public ActionResult<string> Authorize()
    {
        return Ok("Authorized");
    }
}
