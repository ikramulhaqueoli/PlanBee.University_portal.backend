using PlanBee.University_portal.backend.Domain.Commands.Administrative;
using PlanBee.University_portal.backend.Domain.Entities.AcademicSessionDomain;

namespace PlanBee.University_portal.backend.Services.Implementations
{
    public class UniAdministrativeService : IUniAdministrativeService
    {
        private IAcademicSessionWriteRepository _academicSessionWriteRepository;
        private IJwtAuthenticationService _jwtAuthenticationService;

        public UniAdministrativeService(
            IAcademicSessionWriteRepository academicSessionWriteRepository,
            IJwtAuthenticationService jwtAuthenticationService)
        {
            _academicSessionWriteRepository = academicSessionWriteRepository;
            _jwtAuthenticationService = jwtAuthenticationService;
        }

        public Task AddSessionAsync(CreateAcademicSessionCommand command)
        {
            var creatorTokenUser = _jwtAuthenticationService.GetAuthTokenUser();
            var session = new AcademicSession
            {
                YearOne = command.YearOne,
                YearTwo = command.YearTwo,
                YearlySemester = command.YearlySemester
            };

            session.InitiateEntityBase(creatorTokenUser.BaseUserId);
            return _academicSessionWriteRepository.SaveAsync(session);
        }
    }
}
