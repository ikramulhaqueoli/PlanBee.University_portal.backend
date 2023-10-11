using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDesignationDomain;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.QueryHandlers
{
    public class GetEmployeeDesignationsQueryHandler : AbstractQueryHandler<GetEmployeeDesignationsQuery>
    {
        private readonly IEmployeeDesignationReadRepository _employeeDesignationReadRepository;

        public GetEmployeeDesignationsQueryHandler(
            ILogger<GetEmployeeDesignationsQueryHandler> logger,
            IEmployeeDesignationReadRepository employeeDesignationReadRepository)
            : base(logger)
        {
            _employeeDesignationReadRepository = employeeDesignationReadRepository;
        }

        public override async Task<QueryResponse> HandleAsync(GetEmployeeDesignationsQuery query)
        {
            var results = await _employeeDesignationReadRepository
                .GetManyAsync(query.SpecificDesignationIds?.ToList());
            return new QueryResponse
            {
                QueryData = results
            };
        }
    }
}
