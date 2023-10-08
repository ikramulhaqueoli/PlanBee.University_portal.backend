using Newtonsoft.Json;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDesignationDomain;
using PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;

namespace PlanBee.University_portal.backend.Services.Implementations;

public class EmployeeSignupService : IEmployeeSignupService
{
    private readonly IRegistrationRequestWriteRepository _registrationRequestWriteRepository;
    private readonly IBaseUserWriteRepository _baseUserWriteRepository;
    private readonly IEmployeeWriteRepository _employeeWriteRepository;
    private readonly IUniversityEmailService _universityEmailService;
    private readonly IJwtAuthenticationService _jwtAuthenticationService;
    private readonly IEmployeeDesignationReadRepository _employeeDesignationReadRepository;

    public EmployeeSignupService(
        IRegistrationRequestWriteRepository registrationRequestWriteRepository,
        IBaseUserWriteRepository baseUserWriteRepository,
        IEmployeeWriteRepository employeeWriteRepository,
        IUniversityEmailService universityEmailService,
        IJwtAuthenticationService jwtAuthenticationService,
        IEmployeeDesignationReadRepository employeeDesignationReadRepository)
    {
        _registrationRequestWriteRepository = registrationRequestWriteRepository;
        _baseUserWriteRepository = baseUserWriteRepository;
        _employeeWriteRepository = employeeWriteRepository;
        _universityEmailService = universityEmailService;
        _jwtAuthenticationService = jwtAuthenticationService;
        _employeeDesignationReadRepository = employeeDesignationReadRepository;
    }

    public async Task ApproveSignupRequest(RegistrationRequest registrationRequest)
    {
        var employeeSignupCommand = JsonConvert.DeserializeObject<EmployeeSignupCommand>(registrationRequest.CommandJson)!;

        var newBaseUserIdGuid = Guid.Parse(registrationRequest.ItemId);
        var newBaseUser = await SaveGetNewBaseUserAsync(newBaseUserIdGuid, employeeSignupCommand);
        await CreateSaveEmployeeAsync(newBaseUserIdGuid, employeeSignupCommand);

        var approverTokenUser = _jwtAuthenticationService.GetAuthTokenUser();

        var approverDesignation = await _employeeDesignationReadRepository.GetDesignationByUserId(approverTokenUser.BaseUserId);
        if (approverDesignation == null) throw new ItemNotFoundException($"EmployeeDesignation for BaseUserId {approverTokenUser.BaseUserId} not found in the database.");

        await _universityEmailService.SendSignupVerificationAsync(
            fromTokenUser: approverTokenUser,
            toBaseUser: newBaseUser,
            senderDesignation: approverDesignation.Title);
    }

    public async Task SignupAsync(EmployeeSignupCommand command)
    {
        var tokenUser = _jwtAuthenticationService.GetAuthTokenUser();

        var request = new RegistrationRequest
        {
            UserType = UserType.Employee,
            CommandJson = JsonConvert.SerializeObject(command),
            CreatorUserId = tokenUser.BaseUserId,
            CreatorUserRole = string.Join(",", tokenUser.UserRoles?.ToList() ?? new List<UserRole>()),
            ActionStatus = RegistrationActionStatus.Pending
        };

        request.InitiateEntityBase();
        await _registrationRequestWriteRepository.SaveAsync(request);
    }

    private async Task<BaseUser> SaveGetNewBaseUserAsync(Guid baseUserIdGuid, EmployeeSignupCommand employeeSignupCommand)
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
            Gender = Enum.TryParse<Gender>(employeeSignupCommand.Gender, out var gender) ? gender : Gender.Unspecified,
            AccountStatus = AccountStatus.Deactive,
            UserType = UserType.Employee,
        };

        baseUser.InitiateUserWithEntityBase(baseUserIdGuid);
        baseUser.AddRole(UserRole.GeneralEmployee, UserRole.Anonymous);
        baseUser.AddRole(employeeSignupCommand.AdditionalUserRoles);

        await _baseUserWriteRepository.SaveAsync(baseUser);

        return baseUser;
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