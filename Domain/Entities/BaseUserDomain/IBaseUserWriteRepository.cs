namespace PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

public interface IBaseUserWriteRepository
{
    Task SaveAsync(BaseUser user);
}