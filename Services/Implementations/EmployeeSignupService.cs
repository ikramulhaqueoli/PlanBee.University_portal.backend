using Newtonsoft.Json;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Services.Implementations;

public class EmployeeSignupService : IEmployeeSignupService
{
    private readonly IRegistrationRequestWriteRepository _registrationRequestWriteRepository;
    private readonly IBaseUserWriteRepository _baseUserWriteRepository;
    private readonly IEmployeeWriteRepository _employeeWriteRepository;
    private readonly IUniversityEmailService _universityEmailService;

    public EmployeeSignupService(
        IRegistrationRequestWriteRepository registrationRequestWriteRepository,
        IBaseUserWriteRepository baseUserWriteRepository,
        IEmployeeWriteRepository employeeWriteRepository,
        IUniversityEmailService universityEmailService)
    {
        _registrationRequestWriteRepository = registrationRequestWriteRepository;
        _baseUserWriteRepository = baseUserWriteRepository;
        _employeeWriteRepository = employeeWriteRepository;
        _universityEmailService = universityEmailService;
    }

    public async Task ApproveSignupRequest(RegistrationRequest registrationRequest)
    {
        var employeeJson = registrationRequest.ModelDataJson ?? throw new InvalidOperationException($"{nameof(RegistrationRequest.ModelDataJson)} should not be null.");
        var employeeSignupCommand = JsonConvert.DeserializeObject<EmployeeSignupCommand>(employeeJson)!;

        var baseUserIdGuid = Guid.Parse(registrationRequest.ItemId);
        await SaveNewBaseUserAsync(baseUserIdGuid, employeeSignupCommand);
        await CreateSaveEmployeeAsync(baseUserIdGuid, employeeSignupCommand);

        await _universityEmailService.SendSignupVefificationAsync(baseUserIdGuid.ToString());
    }

    public async Task SignupAsync(EmployeeSignupCommand command)
    {
        var request = new RegistrationRequest
        {
            UserType = UserType.Employee,
            ModelDataJson = JsonConvert.SerializeObject(command),
            CreatorUserId = "dummy_creator_user_id",
            CreatorUserRole = "dummy_creator_user_role",
            ActionStatus = RegistrationActionStatus.None
        };

        request.InitiateEntityBase();
        await _registrationRequestWriteRepository.SaveAsync(request);
    }

    private Task SaveNewBaseUserAsync(Guid baseUserIdGuid, EmployeeSignupCommand employeeSignupCommand)
    {
        var baseUser = new BaseUser
        {
            FirstName = employeeSignupCommand.FirstName,
            LastName = employeeSignupCommand.LastName,
            FatherName = employeeSignupCommand.FatherName,
            MotherName = employeeSignupCommand.MotherName,
            MobilePhone = employeeSignupCommand.MobilePhone,
            DateOfBirth = employeeSignupCommand.DateOfBirth,
            UniversityId = employeeSignupCommand.UniversityId,
            NationalId = employeeSignupCommand.NationalId,
            PassportNo = employeeSignupCommand.PassportNo,
            SurName = employeeSignupCommand.SurName,
            PermanentAddress = employeeSignupCommand.PermanentAddress,
            PresentAddress = employeeSignupCommand.PresentAddress,
            AlternatePhone = employeeSignupCommand.AlternatePhone,
            PersonalEmail = employeeSignupCommand.PersonalEmail,
            UniversityEmail = employeeSignupCommand.UniversityEmail,
            Gender = MapGender(employeeSignupCommand.Gender),
            AccountStatus = AccountStatus.Deactive,
            UserType = UserType.Employee,
        };
        baseUser.InitiateUserWithEntityBase(baseUserIdGuid);
        baseUser.AddRole(UserRole.GeneralEmployee, UserRole.Anonymous);

        return _baseUserWriteRepository.SaveAsync(baseUser);
    }

    private static Gender MapGender(string gender)
    {
        if (string.Equals(gender, "Male", StringComparison.OrdinalIgnoreCase)) return Gender.Male;
        else if (string.Equals(gender, "Female", StringComparison.OrdinalIgnoreCase)) return Gender.Female;
        else return Gender.Other;
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
            WorkExperiences = employeeSignupCommand.WorkExperiences
        };
        employee.InitiateEntityBase();

        return _employeeWriteRepository.SaveAsync(employee);
    }
}