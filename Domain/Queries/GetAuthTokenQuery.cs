namespace PlanBee.University_portal.backend.Domain.Queries;

public class GetAuthTokenQuery : AbstractQuery
{
    public string UniversityId { get; set; } = null!;

    public string Password { get; set; } = null!;
}