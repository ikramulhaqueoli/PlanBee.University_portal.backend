using Microsoft.AspNetCore.Mvc.Filters;
using PlanBee.University_portal.backend.Services.Models;

namespace PlanBee.University_portal.backend.Services;

public interface IJwtAuthenticationService
{
    Task<AuthToken?> GetAuthTokenAsync(string universityId, string password);
}