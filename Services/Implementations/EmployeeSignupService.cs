using Newtonsoft.Json;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;
using PlanBee.University_portal.backend.Domain.Enums;

namespace PlanBee.University_portal.backend.Services.Implementations;

public class EmployeeSignupService : IEmployeeSignupService
{
    private readonly IRegistrationRequestWriteRepository _registrationRequestWriteRepository;
    private readonly IBaseUserWriteRepository _baseUserWriteRepository;
    private readonly IEmployeeWriteRepository _employeeWriteRepository;

    public EmployeeSignupService(
        IRegistrationRequestWriteRepository registrationRequestWriteRepository,
        IBaseUserWriteRepository baseUserWriteRepository,
        IEmployeeWriteRepository employeeWriteRepository)
    {
        _registrationRequestWriteRepository = registrationRequestWriteRepository;
        _baseUserWriteRepository = baseUserWriteRepository;
        _employeeWriteRepository = employeeWriteRepository;
    }

    public async Task ApproveSignupRequest(RegistrationRequest registrationRequest)
    {
        var employeeJson = registrationRequest.ModelDataJson ?? throw new InvalidOperationException($"{nameof(RegistrationRequest.ModelDataJson)} should not be null.");
        var employeeSignupCommand = JsonConvert.DeserializeObject<EmployeeSignupCommand>(employeeJson)!;

        var baseUserIdGuid = Guid.NewGuid();
        await CreateSaveBaseUserAsync(baseUserIdGuid);
        await CreateSaveEmployeeAsync(baseUserIdGuid, employeeSignupCommand);
    }

    public async Task SignupAsync(EmployeeSignupCommand command)
    {
        var request = new RegistrationRequest
        {
            UserType = UserType.Employee,
            ModelDataJson = JsonConvert.SerializeObject(command),
            CreatorUserId = "dummy_creator_user_id",
            CreatorUserRole = "dummy_creator_user_role",
            ActionStatus = RequestActionStatus.None
        };

        request.InitiateEntityBase();
        await _registrationRequestWriteRepository.SaveAsync(request);
    }

    private Task CreateSaveBaseUserAsync(Guid baseUserIdGuid)
    {
        var baseUser = new BaseUser
        {
            UserType = UserType.Employee
        };
        baseUser.InitiateUserWithEntityBase(baseUserIdGuid);
        baseUser.AddRole(UserRole.GeneralEmployee, UserRole.Anonymous);

        return _baseUserWriteRepository.SaveAsync(baseUser);
    }

    private Task CreateSaveEmployeeAsync(Guid baseUserIdGuid, EmployeeSignupCommand employeeSignupCommand)
    {
        var employee = new Employee
        {
            JoiningDate = employeeSignupCommand.JoiningDate,
            WorkplaceId = employeeSignupCommand.WorkplaceId,
            DesignationId = employeeSignupCommand.DesignationId,
            BaseUserId = baseUserIdGuid.ToString(),
            EducationalQualifications = employeeSignupCommand.EducationalQualifications,
            WordExperiences = employeeSignupCommand.WordExperiences
        };
        employee.InitiateEntityBase();

        return _employeeWriteRepository.SaveAsync(employee);
    }
}