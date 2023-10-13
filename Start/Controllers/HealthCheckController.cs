using Microsoft.AspNetCore.Mvc;
using PlanBee.University_portal.backend.Start.Attributes;

namespace PlanBee.University_portal.backend.Start.Controllers;

[ApiController]
public class HealthCheckController : ControllerBase
{
    [HttpGet("health")]
    public ActionResult<string> Get()
    {
        return Ok("Healthy");
    }

    [Anonymous]
    [HttpGet("health/anonymous")]
    public ActionResult<string> Authorize()
    {
        return Ok("Authorized");
    }

    [HttpGet("test")]
    public async Task<ActionResult<string>> TestAsync()
    {
        return Ok("test success");
    }
}
