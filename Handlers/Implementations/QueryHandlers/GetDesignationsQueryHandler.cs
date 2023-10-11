using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.DesignationDomain;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.QueryHandlers
{
    public class GetDesignationsQueryHandler : AbstractQueryHandler<GetDesignationsQuery>
    {
        private readonly IDesignationReadRepository _designationReadRepository;

        public GetDesignationsQueryHandler(
            ILogger<GetDesignationsQueryHandler> logger,
            IDesignationReadRepository designationReadRepository)
            : base(logger)
        {
            _designationReadRepository = designationReadRepository;
        }

        public override async Task<QueryResponse> HandleAsync(GetDesignationsQuery query)
        {
            var results = await _designationReadRepository
                .GetManyAsync(query.SpecificItemIds?.ToList());
            return new QueryResponse
            {
                QueryData = results
            };
        }
    }
}
