namespace PlanBee.University_portal.backend.Domain.Models;

public class AuthTokenResponse
{
    public string Token { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;
}