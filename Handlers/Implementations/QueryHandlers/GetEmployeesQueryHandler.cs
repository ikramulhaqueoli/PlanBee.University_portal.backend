using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDesignationDomain;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Repositories;

namespace PlanBee.University_portal.backend.Handlers.Implementations.QueryHandlers
{
    public class GetEmployeesQueryHandler : AbstractQueryHandler<GetEmployeesQuery>
    {
        private readonly IMongoReadRepository _mongoReadRepository;

        public GetEmployeesQueryHandler(
            ILogger<GetAuthTokenQueryHandler> logger,
            IMongoReadRepository mongoReadRepository) : base(logger)
        {
            _mongoReadRepository = mongoReadRepository;
        }

        public override async Task<QueryResponse> HandleAsync(GetEmployeesQuery query)
        {
            var employeeFilter = Builders<Employee>.Filter.Empty;
            var userFilter = Builders<BaseUser>.Filter.Empty;
            if (query.SpecificBaseUserIds?.Any() == true)
            {
                employeeFilter &= Builders<Employee>.Filter.In(
                    nameof(Employee.BaseUserId),
                    query.SpecificBaseUserIds);

                userFilter &= Builders<BaseUser>.Filter.In(
                    nameof(BaseUser.ItemId),
                    query.SpecificBaseUserIds);
            }

            var employees = await _mongoReadRepository.GetAsync(employeeFilter);
            var baseUsers = await _mongoReadRepository.GetAsync(userFilter);

            var queryResponse = new QueryResponse
            {
                QueryData = employees.Select(employee =>
                new
                {
                    Employee = employee,
                    User = baseUsers.FirstOrDefault(user => user.ItemId == employee.BaseUserId)
                })
            };

            return queryResponse;
        }
    }
}
