using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.StudentDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Services.Implementations
{
    public class StudentSignupService : ISpecificSignupService
    {
        private readonly IStudentWriteRepository _studentWriteRepository;

        public StudentSignupService(
            IStudentWriteRepository studentWriteRepository)
        {
            _studentWriteRepository = studentWriteRepository;
        }

        public UserType UserType => UserType.Student;

        public Task CreateAsync(string baseUserId, AbstractSignupRequestCommand signupRequestCommand, string creatorBaseUserId)
        {
            var studentSignupRequestCommand = (StudentSignupRequestCommand)signupRequestCommand;
            var student = new Student
            {
                AdmissionDate = studentSignupRequestCommand.AdmissionDate,
                DepartmentId = studentSignupRequestCommand.DepartmentId,
                HallName = studentSignupRequestCommand.HallName,
                Session = studentSignupRequestCommand.Session,
                RecidenceStatus = studentSignupRequestCommand.RecidenceStatus,
                BaseUserId = baseUserId
            };

            student.InitiateEntityBase(creatorBaseUserId);
            return _studentWriteRepository.SaveAsync(student);
        }
    }
}
