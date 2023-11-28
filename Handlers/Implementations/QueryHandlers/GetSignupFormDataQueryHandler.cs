using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.AcademicSessionDomain;
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
        private readonly IAcademicSessionReadRepository _academicSessionReadRepository;

        public GetSignupFormDataQueryHandler(
            ILogger<GetSignupFormDataQueryHandler> logger,
            IWorkplaceReadRepository workplaceReadRepository,
            IDesignationReadRepository designationReadRepository,
            IDepartmentReadRepository departmentReadRepository,
            IAcademicSessionReadRepository academicSessionReadRepository)
            : base(logger)
        {
            _workplaceReadRepository = workplaceReadRepository;
            _designationReadRepository = designationReadRepository;
            _departmentReadRepository = departmentReadRepository;
            _academicSessionReadRepository = academicSessionReadRepository;
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
                    AcademicWorkplaces = workplaces.Where(workplace => workplace.WorkplaceType == WorkplaceType.Academic),
                    NonAcademicWorkplaces = workplaces.Where(workplace => workplace.WorkplaceType == WorkplaceType.NonAcademic),
                    AcademicDesignations = designations.Where(workplace => workplace.DesignationType == DesignationType.Academic),
                    NonAcademicDesignations = designations.Where(workplace => workplace.DesignationType == DesignationType.NonAcademic),
                }
            };

            return response;
        }

        private async Task<QueryResponse> GetResponseForStudentAsync()
        {
            var departments = await _departmentReadRepository.GetManyAsync();
            var sessions = await _academicSessionReadRepository.GetAllAsync();

            var workplaceIds = departments.Select(department => department.WorkplaceId).ToList();
            var workplaces = await _workplaceReadRepository.GetManyAsync(workplaceIds);

            var departmentData = departments.Select(department => 
            {
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

            var sessionData = sessions.Select(session =>
            {
                return new
                {
                    Session = session.Title
                };
            });

            return new QueryResponse
            {
                QueryData = new
                {
                    Departments = departmentData,
                    Sessions = sessionData,
                }
            };
        }
    }
}
