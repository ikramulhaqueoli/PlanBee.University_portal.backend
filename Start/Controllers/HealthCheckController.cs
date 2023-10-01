using Microsoft.AspNetCore.Mvc;
using PlanBee.University_portal.backend.Start.Attributes;

namespace PlanBee.University_portal.backend.Start.Controllers;

[ApiController]
public class HealthCheckController : ControllerBase
{
    [HttpGet("healthcheck")]
    public ActionResult<string> Get()
    {
        return Ok("Healthy");
    }

    [Anonymous]
    [HttpGet("healthcheck/anonymous")]
    public ActionResult<string> Authorize()
    {
        return Ok("Authorized");
    }

    [HttpGet("test")]
    public async Task<ActionResult<string>> Test()
    {
        /*var userWriteRepository = Activator.CreateInstance<IBaseUserWriteRepository>();

        var baseUser = new BaseUser();
        baseUser.Initiate();
        await userWriteRepository.SaveAsync(baseUser);*/
        return Ok("test success");
    }
}
