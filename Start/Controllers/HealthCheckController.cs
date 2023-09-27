using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PlanBee.University_portal.backend.Start.Controllers;

[ApiController]
public class HealthCheckController : ControllerBase
{
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
