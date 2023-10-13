using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.QueryHandlers
{
    internal class GetRegistrationRequestsQueryHandler : AbstractQueryHandler<GetRegistrationRequestsQuery>
    {
        private readonly IRegistrationRequestReadRepository _registrationRequestReadRepository;

        public GetRegistrationRequestsQueryHandler(
            ILogger<GetRegistrationRequestsQueryHandler> logger, 
            IRegistrationRequestReadRepository registrationRequestReadRepository) : base(logger)
        {
            _registrationRequestReadRepository = registrationRequestReadRepository;
        }

        public override async Task<QueryResponse> HandleAsync(GetRegistrationRequestsQuery query)
        {
            var regustrationRequests = await _registrationRequestReadRepository
                .GetWithViewsAsync(query.SpecificItemIds, query.UserType);

            var queryResponse = new QueryResponse
            {
                QueryData = regustrationRequests,
            };

            return queryResponse;
        }
    }
}
