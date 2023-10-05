namespace PlanBee.University_portal.backend.Domain.Entities.UniTemplateDomain
{
    public class UniTemplate : EntityBase
    {
        public string Key { get; set; } = null!;

        public string Body { get; set; } = null!;

        public string? Subject { get; set; }
    }
}
