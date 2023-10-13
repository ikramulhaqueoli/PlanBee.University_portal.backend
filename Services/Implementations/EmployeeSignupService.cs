using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Services.Implementations;

public class EmployeeSignupService : ISpecificSignupService
{
    private readonly IEmployeeWriteRepository _employeeWriteRepository;

    public EmployeeSignupService(
        IEmployeeWriteRepository employeeWriteRepository)
    {
        _employeeWriteRepository = employeeWriteRepository;
    }

    public UserType UserType => UserType.Employee;

    public Task CreateAsync(
        string baseUserId,
        AbstractSignupRequestCommand signupRequestCommand)
    {
        var employeeSignupRequestCommand = (EmployeeSignupRequestCommand)signupRequestCommand;
        var employee = new Employee
        {
            JoiningDate = employeeSignupRequestCommand.JoiningDate,
            WorkplaceId = employeeSignupRequestCommand.WorkplaceId,
            DesignationId = employeeSignupRequestCommand.DesignationId,
            BaseUserId = baseUserId,
            EducationalQualifications = signupRequestCommand.EducationalQualifications,
            WorkExperiences = employeeSignupRequestCommand.WorkExperiences
        };
        employee.InitiateEntityBase();

        return _employeeWriteRepository.SaveAsync(employee);
    }
}