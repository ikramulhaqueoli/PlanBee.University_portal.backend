using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDesignationDomain;
using PlanBee.University_portal.backend.Domain.Entities.WorkplaceDomain;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.QueryHandlers
{
    public class GetSignupFormDataQueryHandler : AbstractQueryHandler<GetSignupFormDataQuery>
    {
        private readonly IWorkplaceReadRepository _workplaceReadRepository;
        private readonly IEmployeeDesignationReadRepository _employeeDesignationReadRepository;

        public GetSignupFormDataQueryHandler(
            ILogger<GetSignupFormDataQueryHandler> logger,
            IWorkplaceReadRepository workplaceReadRepository,
            IEmployeeDesignationReadRepository employeeDesignationReadRepository)
            : base(logger)
        {
            _workplaceReadRepository = workplaceReadRepository;
            _employeeDesignationReadRepository = employeeDesignationReadRepository;
        }

        public override async Task<QueryResponse> HandleAsync(GetSignupFormDataQuery query)
        {
            var workplaces = await _workplaceReadRepository.GetActiveAsync();
            var designations = await _employeeDesignationReadRepository.GetActiveAsync();
            var response = new QueryResponse
            {
                QueryData = new
                {
                    AcademicWorkplaces = workplaces.Where(workplace => workplace.WorkplaceType == Domain.Enums.Business.WorkplaceType.Academic),
                    NonAcademicWorkplaces = workplaces.Where(workplace => workplace.WorkplaceType == Domain.Enums.Business.WorkplaceType.NonAcademic),
                    AcademicDesignations = designations.Where(workplace => workplace.DesignationType == Domain.Enums.Business.DesignationType.NonAcademic),
                    NonAcademicDesignations = designations.Where(workplace => workplace.DesignationType == Domain.Enums.Business.DesignationType.NonAcademic),
                }
            };

            return response;
        }
    }
}
