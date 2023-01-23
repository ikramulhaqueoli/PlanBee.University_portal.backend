using PlanBee.University_portal.backend.Domain.Enums;

namespace PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

public class RegistrationRequest : EntityBase
{
    public string EntityType { get; set; } = null!;

    public string ModelDataJson { get; set; } = null!;

    public string CreatorUserId { get; set; } = null!;

    public string CreatorUserRole { get; set; } = null!;

    public RequestActionStatus ActionStatus { get; set; }

    public string ActionComment { get; set; } = string.Empty;
}