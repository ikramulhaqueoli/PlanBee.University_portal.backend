using PlanBee.University_portal.backend.Services.Models;

namespace PlanBee.University_portal.backend.Services;

public interface IJwtAuthenticationService
{
    Task<AuthToken?> Authenticate(string registrationId, string password);
}