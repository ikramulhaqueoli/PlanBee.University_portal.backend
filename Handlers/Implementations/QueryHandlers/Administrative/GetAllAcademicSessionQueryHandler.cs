using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Entities.AcademicSessionDomain;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.QueryHandlers.Administrative
{
    public class GetAllAcademicSessionQueryHandler : AbstractQueryHandler<GetAllAcademicSessionQuery>
    {
        private readonly IAcademicSessionReadRepository _academicSessionReadRepository;

        public GetAllAcademicSessionQueryHandler(
            ILogger<GetAllAcademicSessionQueryHandler> logger,
            IAcademicSessionReadRepository academicSessionReadRepository) : base(logger)
        {
            _academicSessionReadRepository = academicSessionReadRepository;
        }

        public override async Task<QueryResponse> HandleAsync(GetAllAcademicSessionQuery query)
        {
            var academicSessions = await _academicSessionReadRepository.GetAllAsync(query.ActiveOnly);
            return new QueryResponse() { QueryData = academicSessions };
        }
    }
}
