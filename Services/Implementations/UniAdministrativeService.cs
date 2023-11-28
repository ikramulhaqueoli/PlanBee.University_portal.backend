using PlanBee.University_portal.backend.Domain.Commands.Administrative;
using PlanBee.University_portal.backend.Domain.Entities.AcademicSessionDomain;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;

namespace PlanBee.University_portal.backend.Services.Implementations
{
    public class UniAdministrativeService : IUniAdministrativeService
    {
        private readonly IAcademicSessionWriteRepository _academicSessionWriteRepository;
        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        private readonly IAcademicSessionReadRepository _academicSessionReadRepository;

        public UniAdministrativeService(
            IAcademicSessionWriteRepository academicSessionWriteRepository,
            IJwtAuthenticationService jwtAuthenticationService,
            IAcademicSessionReadRepository academicSessionReadRepository)
        {
            _academicSessionWriteRepository = academicSessionWriteRepository;
            _jwtAuthenticationService = jwtAuthenticationService;
            _academicSessionReadRepository = academicSessionReadRepository;
        }

        public async Task AddSessionAsync(CreateAcademicSessionCommand command)
        {
            var sessionExists = await _academicSessionReadRepository.SessionExistsAsync(command.Title);
            if (sessionExists) throw new ItemAlreadyExistsException($"Session with title {command.Title} already exists in the database.");
            
            var session = new AcademicSession
            {
                Title = command.Title
            };

            var creatorTokenUser = _jwtAuthenticationService.GetAuthTokenUser();
            session.InitiateEntityBase(creatorTokenUser.BaseUserId);
            await _academicSessionWriteRepository.SaveAsync(session);
        }
    }
}
