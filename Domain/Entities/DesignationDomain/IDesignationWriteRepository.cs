namespace PlanBee.University_portal.backend.Domain.Entities.DesignationDomain
{
    public interface IDesignationWriteRepository
    {
        public Task SaveAsync(Designation designation);
    }
}
