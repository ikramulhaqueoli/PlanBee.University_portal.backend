using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.DesignationDomain;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain;
using PlanBee.University_portal.backend.Domain.Entities.WorkplaceDomain;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Repositories;

namespace PlanBee.University_portal.backend.Handlers.Implementations.QueryHandlers
{
    public class GetEmployeesQueryHandler : AbstractQueryHandler<GetEmployeesQuery>
    {
        private readonly IMongoReadRepository _mongoReadRepository;
        private readonly IDesignationReadRepository _designationReadRepository;
        private readonly IWorkplaceReadRepository _workplaceReadRepository;

        public GetEmployeesQueryHandler(
            ILogger<GetAuthTokenQueryHandler> logger,
            IMongoReadRepository mongoReadRepository,
            IDesignationReadRepository designationReadRepository,
            IWorkplaceReadRepository workplaceReadRepository) : base(logger)
        {
            _mongoReadRepository = mongoReadRepository;
            _designationReadRepository = designationReadRepository;
            _workplaceReadRepository = workplaceReadRepository;
        }

        public override async Task<QueryResponse> HandleAsync(GetEmployeesQuery query)
        {
            var employeeFilter = Builders<Employee>.Filter.Empty;
            var userFilter = Builders<BaseUser>.Filter.Empty;
            if (query.SpecificBaseUserIds != null)
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

            var designationIds = employees.Select(employee => employee.DesignationId).ToList();
            var designations = await _designationReadRepository.GetManyAsync(designationIds);

            var workplaceIds = employees.Select(employee => employee.WorkplaceId).ToList();
            var workplaces = await _workplaceReadRepository.GetManyAsync(workplaceIds);

            var queryResponse = new QueryResponse
            {
                QueryData = employees.Select(employee =>
                new
                {
                    Employee = employee,
                    User = baseUsers.FirstOrDefault(
                        user => user.ItemId == employee.BaseUserId),
                    Designation = designations.FirstOrDefault(
                        designation => designation.ItemId == employee.DesignationId),
                    Workplace = workplaces.FirstOrDefault(
                        workplace => workplace.ItemId == employee.WorkplaceId)
                })
            };

            return queryResponse;
        }
    }
}
