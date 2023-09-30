using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.QueryHandlers
{
    public class GetAllEmployeeQueryHandler : AbstractQueryHandler<GetAllEmployeeQuery>
    {
        private readonly IEmployeeReadRepository _employeeReadRepository;

        public GetAllEmployeeQueryHandler(
            ILogger<GetAuthTokenQueryHandler> logger,
            IEmployeeReadRepository employeeReadRepository) : base(logger)
        {
            _employeeReadRepository = employeeReadRepository;
        }

        public override async Task<QueryResponse> HandleAsync(GetAllEmployeeQuery query)
        {
            var employees = await _employeeReadRepository.GetAllAsync();
            var queryResponse = new QueryResponse
            {
                QueryData = employees,
            };

            return queryResponse;
        }
    }
}
