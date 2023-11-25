using PlanBee.University_portal.backend.Domain.Commands.Administrative;

namespace PlanBee.University_portal.backend.Services
{
    public interface IUniAdministrativeService
    {
        Task AddSessionAsync(CreateAcademicSessionCommand command);
    }
}
