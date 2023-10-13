using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.DepartmentDomain;
using PlanBee.University_portal.backend.Domain.Entities.DesignationDomain;
using PlanBee.University_portal.backend.Domain.Entities.StudentDomain;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;
using PlanBee.University_portal.backend.Repositories;

namespace PlanBee.University_portal.backend.Handlers.Implementations.QueryHandlers
{
    public class GetStudentQueryHandler : AbstractQueryHandler<GetStudentQuery>
    {
        private readonly IMongoReadRepository _mongoReadRepository;
        private readonly IDesignationReadRepository _designationReadRepository;
        private readonly IDepartmentReadRepository _departmentReadRepository;

        public GetStudentQueryHandler(
            ILogger<GetAuthTokenQueryHandler> logger,
            IMongoReadRepository mongoReadRepository,
            IDesignationReadRepository designationReadRepository,
            IDepartmentReadRepository departmentReadRepository)
            : base(logger)
        {
            _mongoReadRepository = mongoReadRepository;
            _designationReadRepository = designationReadRepository;
            _departmentReadRepository = departmentReadRepository;
        }

        public override async Task<QueryResponse> HandleAsync(GetStudentQuery query)
        {
            var studentFilter = Builders<Student>.Filter.Empty;
            var userFilter = Builders<BaseUser>.Filter.Empty;
            if (query.SpecificBaseUserIds != null)
            {
                studentFilter &= Builders<Student>.Filter.In(
                    nameof(Student.BaseUserId),
                    query.SpecificBaseUserIds);

                userFilter &= Builders<BaseUser>.Filter.In(
                    nameof(BaseUser.ItemId),
                    query.SpecificBaseUserIds);
            }

            var students = await _mongoReadRepository.GetAsync(studentFilter);
            var baseUsers = await _mongoReadRepository.GetAsync(userFilter);

            var designationIds = students.Select(student => student.DepartmentId).ToList();
            var designations = await _designationReadRepository.GetManyAsync(designationIds);

            var departmentIds = students.Select(student => student.DepartmentId).ToList();
            var departments = await _departmentReadRepository.GetManyAsync(departmentIds);

            var queryResponse = new QueryResponse
            {
                QueryData = students.Select(student =>
                new
                {
                    Student = student,
                    User = baseUsers.FirstOrDefault(
                        user => user.ItemId == student.BaseUserId),
                    Department = departments.FirstOrDefault(
                        department => department.ItemId == student.DepartmentId)
                })
            };

            return queryResponse;
        }
    }
}
