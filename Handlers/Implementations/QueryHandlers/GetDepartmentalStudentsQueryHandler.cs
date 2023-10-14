using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.DepartmentDomain;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain;
using PlanBee.University_portal.backend.Domain.Entities.StudentDomain;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Services;

namespace PlanBee.University_portal.backend.Handlers.Implementations.QueryHandlers
{
    internal class GetDepartmentalStudentsQueryHandler
        : AbstractQueryHandler<GetDepartmentalStudentsQuery>
    {
        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        private readonly IEmployeeReadRepository _employeeReadRepository;
        private readonly IStudentReadRepository _studentReadRepository;
        private readonly IDepartmentReadRepository _departmentReadRepository;
        private readonly IBaseUserReadRepository _baseUserReadRepository;

        public GetDepartmentalStudentsQueryHandler(
            ILogger<GetDepartmentalStudentsQueryHandler> logger,
            IJwtAuthenticationService jwtAuthenticationService,
            IEmployeeReadRepository employeeReadRepository,
            IStudentReadRepository studentReadRepository,
            IDepartmentReadRepository departmentReadRepository,
            IBaseUserReadRepository baseUserReadRepository)
            : base(logger)
        {
            _jwtAuthenticationService = jwtAuthenticationService;
            _employeeReadRepository = employeeReadRepository;
            _studentReadRepository = studentReadRepository;
            _departmentReadRepository = departmentReadRepository;
            _baseUserReadRepository = baseUserReadRepository;
        }

        public override async Task<QueryResponse> HandleAsync(GetDepartmentalStudentsQuery query)
        {
            var tokenUser = _jwtAuthenticationService.GetAuthTokenUser();
            var employee = await _employeeReadRepository.GetByUserIdAsync(tokenUser.BaseUserId)
                ?? throw new ItemNotFoundException($"Employee with BaseUserId: {tokenUser.BaseUserId} not found in the database.");
            var department = await _departmentReadRepository.GetByWorkplaceIdAsync(employee.WorkplaceId)
                ?? throw new ItemNotFoundException($"Department with WorkplaceId: {employee.WorkplaceId} not found in the database.");
            var students = await _studentReadRepository.GetByDepartmentId(department.ItemId);

            var fetchedBaseUserIds = students.Select(student => student.BaseUserId).ToList();
            var baseUsers = await _baseUserReadRepository.GetManyAsync(fetchedBaseUserIds);

            return new QueryResponse
            {
                QueryData = students.Select(student =>
                {
                    var baseUser = baseUsers.FirstOrDefault(user => user.ItemId == student.BaseUserId);
                    return new
                    {
                        Student = student,
                        User = baseUser,
                    };
                }),
            };
        }
    }
}
