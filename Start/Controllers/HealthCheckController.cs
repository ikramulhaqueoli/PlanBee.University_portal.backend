using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PlanBee.University_portal.backend.Start.Controllers;

[ApiController]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class HealthCheckController : ControllerBase
{
    private readonly ILogger<HealthCheckController> _logger;

    public HealthCheckController(ILogger<HealthCheckController> logger)
    {
        _logger = logger;
    }

    [HttpGet(template: "healthcheck")]
    public ActionResult<string> Get()
    {
        return Ok("Healthy");
    }
    
    [HttpGet(template: "healthcheck/authorize")]
    [Authorize]
    public ActionResult<string> Authorize()
    {
        return Ok("Authorized");
    }
}