using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.DepartmentDomain;
using PlanBee.University_portal.backend.Domain.Entities.DesignationDomain;
using PlanBee.University_portal.backend.Domain.Entities.WorkplaceDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.QueryHandlers
{
    public class GetSignupFormDataQueryHandler : AbstractQueryHandler<GetSignupFormDataQuery>
    {
        private readonly IWorkplaceReadRepository _workplaceReadRepository;
        private readonly IDesignationReadRepository _designationReadRepository;
        private readonly IDepartmentReadRepository _departmentReadRepository;

        public GetSignupFormDataQueryHandler(
            ILogger<GetSignupFormDataQueryHandler> logger,
            IWorkplaceReadRepository workplaceReadRepository,
            IDesignationReadRepository designationReadRepository,
            IDepartmentReadRepository departmentReadRepository)
            : base(logger)
        {
            _workplaceReadRepository = workplaceReadRepository;
            _designationReadRepository = designationReadRepository;
            _departmentReadRepository = departmentReadRepository;
        }

        public override async Task<QueryResponse> HandleAsync(GetSignupFormDataQuery query)
        {
            if (query.UserType == UserType.Employee)
            {
                return await GetResponseForEmployeeAsync();
            }

            if (query.UserType == UserType.Student)
            {
                return await GetResponseForStudentAsync();
            }

            var errorQueryResponse = new QueryResponse();
            errorQueryResponse.SetQueryError(new InvalidRequestArgumentException("Payload contains Invalid UserType."));
            return errorQueryResponse;
        }

        private async Task<QueryResponse> GetResponseForEmployeeAsync()
        {
            var workplaces = await _workplaceReadRepository.GetManyAsync();
            var designations = await _designationReadRepository.GetManyAsync(activeOnly: true);
            var response = new QueryResponse
            {
                QueryData = new
                {
                    AcademicWorkplaces = workplaces.Where(workplace => workplace.WorkplaceType == Domain.Enums.Business.WorkplaceType.Academic),
                    NonAcademicWorkplaces = workplaces.Where(workplace => workplace.WorkplaceType == Domain.Enums.Business.WorkplaceType.NonAcademic),
                    AcademicDesignations = designations.Where(workplace => workplace.DesignationType == Domain.Enums.Business.DesignationType.Academic),
                    NonAcademicDesignations = designations.Where(workplace => workplace.DesignationType == Domain.Enums.Business.DesignationType.NonAcademic),
                }
            };

            return response;
        }

        private async Task<QueryResponse> GetResponseForStudentAsync()
        {
            var departments = await _departmentReadRepository.GetManyAsync();

            var workplaceIds = departments.Select(department => department.WorkplaceId).ToList();
            var workplaces = await _workplaceReadRepository.GetManyAsync(workplaceIds);

            var queryData = departments.Select(department => {
                var workplace = workplaces.FirstOrDefault(
                    workplace => workplace.ItemId == department.WorkplaceId);

                return new
                {
                    DepartmentId = department.ItemId,
                    Title = workplace?.Title,
                    TitleAcronym = workplace?.TitleAcronym,
                    WorkplaceId = workplace?.ItemId
                };
            });

            var response = new QueryResponse
            {
                QueryData = new
                {
                    Departments = departments
                }
            };

            return response;
        }
    }
}
