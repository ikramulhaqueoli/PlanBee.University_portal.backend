using Newtonsoft.Json;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.DesignationDomain;
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
    private readonly IDesignationReadRepository _designationReadRepository;
    private readonly IBaseUserReadRepository _baseUserReadRepository;

    public EmployeeSignupService(
        IRegistrationRequestWriteRepository registrationRequestWriteRepository,
        IBaseUserWriteRepository baseUserWriteRepository,
        IEmployeeWriteRepository employeeWriteRepository,
        IUniversityEmailService universityEmailService,
        IJwtAuthenticationService jwtAuthenticationService,
        IDesignationReadRepository designationReadRepository,
        IBaseUserReadRepository baseUserReadRepository)
    {
        _registrationRequestWriteRepository = registrationRequestWriteRepository;
        _baseUserWriteRepository = baseUserWriteRepository;
        _employeeWriteRepository = employeeWriteRepository;
        _universityEmailService = universityEmailService;
        _jwtAuthenticationService = jwtAuthenticationService;
        _designationReadRepository = designationReadRepository;
        _baseUserReadRepository = baseUserReadRepository;
    }

    public async Task ApproveSignupRequest(RegistrationRequest registrationRequest)
    {
        var employeeSignupCommand = JsonConvert.DeserializeObject<EmployeeSignupCommand>(registrationRequest.CommandJson)!;

        var newBaseUser = await SaveGetNewBaseUserAsync(employeeSignupCommand);
        await CreateSaveEmployeeAsync(newBaseUser.ItemId, employeeSignupCommand);

        var approverTokenUser = _jwtAuthenticationService.GetAuthTokenUser();

        var approverDesignation = await _designationReadRepository.GetDesignationByUserIdAsync(approverTokenUser.BaseUserId) ?? throw new ItemNotFoundException($"Designation for BaseUserId {approverTokenUser.BaseUserId} not found in the database.");
        
        await _universityEmailService.SendSignupVerificationAsync(
            fromTokenUser: approverTokenUser,
            toBaseUser: newBaseUser,
            senderDesignation: approverDesignation.Title);
    }

    public async Task SignupAsync(EmployeeSignupCommand command)
    {
        var creatorTokenUser = _jwtAuthenticationService.GetAuthTokenUser();
        var creatorDesignation = await _designationReadRepository.GetDesignationByUserIdAsync(creatorTokenUser.BaseUserId) ?? throw new ItemNotFoundException($"Designation for BaseUserId {creatorTokenUser.BaseUserId} not found in the database.");
        var newEmpDesignation = await _designationReadRepository.GetAsync(command.DesignationId) ?? throw new ItemNotFoundException($"Designation with Id {command.DesignationId} not found in the database.");
        command.DesignationTitle ??= newEmpDesignation.Title;
        command.DesignationType ??= newEmpDesignation.DesignationType;

        var request = new RegistrationRequest
        {
            UserType = UserType.Employee,
            CommandJson = JsonConvert.SerializeObject(command),
            CreatorUserId = creatorTokenUser.BaseUserId,
            CreatorUserRoles = creatorTokenUser.UserRoles ?? Array.Empty<UserRole>(),
            ActionStatus = RegistrationActionStatus.Pending,
            CreatorDesignationId = creatorDesignation.ItemId
        };

        request.InitiateEntityBase();
        await _registrationRequestWriteRepository.SaveAsync(request);
    }

    private async Task<BaseUser> SaveGetNewBaseUserAsync(EmployeeSignupCommand employeeSignupCommand)
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

        baseUser.InitiateUserWithEntityBase();
        baseUser.AddRole(UserRole.GeneralEmployee, UserRole.Anonymous);
        baseUser.AddRole(employeeSignupCommand.AdditionalUserRoles);

        await _baseUserWriteRepository.SaveAsync(baseUser);

        return baseUser;
    }

    private Task CreateSaveEmployeeAsync(string baseUserId, EmployeeSignupCommand employeeSignupCommand)
    {
        var employee = new Employee
        {
            JoiningDate = employeeSignupCommand.JoiningDate,
            WorkplaceId = employeeSignupCommand.WorkplaceId,
            DesignationId = employeeSignupCommand.DesignationId,
            BaseUserId = baseUserId,
            EducationalQualifications = employeeSignupCommand.EducationalQualifications,
            WorkExperiences = employeeSignupCommand.WorkExperiences
        };
        employee.InitiateEntityBase();

        return _employeeWriteRepository.SaveAsync(employee);
    }
}