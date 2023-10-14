using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.WorkplaceDomain;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Repositories;

namespace PlanBee.University_portal.backend.Handlers.Implementations.QueryHandlers
{
    public class GetWorkplacesQueryHandler : AbstractQueryHandler<GetWorkplacesQuery>
    {
        private readonly IMongoReadRepository _mongoReadRepository;

        public GetWorkplacesQueryHandler(
            ILogger<GetWorkplacesQueryHandler> logger,
            IMongoReadRepository mongoReadRepository) : base(logger)
        {
            _mongoReadRepository = mongoReadRepository;
        }

        public override async Task<QueryResponse> HandleAsync(GetWorkplacesQuery query)
        {
            var filter = Builders<Workplace>.Filter.Empty;
            if (query.SpecificItemIds != null)
            {
                filter &= Builders<Workplace>.Filter.In(
                    nameof(Workplace.ItemId),
                query.SpecificItemIds);
            }

            var results = await _mongoReadRepository.GetManyAsync(filter);
            return new QueryResponse
            {
                QueryData = results
            };
        }
    }
}
