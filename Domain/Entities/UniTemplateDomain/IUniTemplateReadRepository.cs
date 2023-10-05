namespace PlanBee.University_portal.backend.Domain.Entities.UniTemplateDomain
{
    public interface IUniTemplateReadRepository
    {
        public Task<UniTemplate?> GetByKeyAsync(string key);
    }
}
