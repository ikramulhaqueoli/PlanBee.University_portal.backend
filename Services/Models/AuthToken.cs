namespace PlanBee.University_portal.backend.Services.Models;

public class AuthToken
{
    public string Token { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;
}