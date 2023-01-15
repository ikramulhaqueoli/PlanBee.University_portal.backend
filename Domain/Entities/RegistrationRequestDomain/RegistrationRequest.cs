using PlanBee.University_portal.backend.Domain.Enums;

namespace PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

public class RegistrationRequest : EntityBase
{
    public string EntityType { get; set; }

    public string ModelDataJson { get; set; }

    public string CreatorUserId { get; set; }

    public string CreatorUserRole { get; set; }

    public RequestActionStatus ActionStatus { get; set; }

    public string ActionComment { get; set; } = string.Empty;
}