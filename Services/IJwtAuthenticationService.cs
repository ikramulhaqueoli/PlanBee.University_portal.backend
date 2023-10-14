using PlanBee.University_portal.backend.Domain.Models;

namespace PlanBee.University_portal.backend.Services;

public interface IJwtAuthenticationService
{
    Task<AuthTokenResponse?> GetAuthTokenAsync(
        string emailOrUniversityId,
        string password);

    AuthTokenUser GetAuthTokenUser();
}