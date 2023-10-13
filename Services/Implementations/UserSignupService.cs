using Newtonsoft.Json;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.DesignationDomain;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Exceptions.SystemExceptions;

namespace PlanBee.University_portal.backend.Services.Implementations
{
    public class UserSignupService : IUserSignupService
    {
        private readonly IBaseUserWriteRepository _baseUserWriteRepository;
        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        private readonly IDesignationReadRepository _designationReadRepository;
        private readonly IUniversityEmailService _universityEmailService;
        private readonly IEnumerable<ISpecificSignupService> _specificSignupServices;
        private readonly IRegistrationRequestWriteRepository _registrationRequestWriteRepository;

        public UserSignupService(
            IBaseUserWriteRepository baseUserWriteRepository,
            IJwtAuthenticationService jwtAuthenticationService,
            IDesignationReadRepository designationReadRepository,
            IUniversityEmailService universityEmailService,
            IEnumerable<ISpecificSignupService> specificSignupServices,
            IRegistrationRequestWriteRepository registrationRequestWriteRepository)
        {
            _baseUserWriteRepository = baseUserWriteRepository;
            _jwtAuthenticationService = jwtAuthenticationService;
            _designationReadRepository = designationReadRepository;
            _universityEmailService = universityEmailService;
            _specificSignupServices = specificSignupServices;
            _registrationRequestWriteRepository = registrationRequestWriteRepository;
        }

        public async Task ApproveSignupRequestAsync(RegistrationRequest registrationRequest)
        {
            AbstractSignupRequestCommand? signupRequestCommand = null;

            if (registrationRequest.UserType == UserType.Employee)
                signupRequestCommand = JsonConvert.DeserializeObject<EmployeeSignupRequestCommand>(registrationRequest.CommandJson)!;
            else if (registrationRequest.UserType == UserType.Student)
                signupRequestCommand = JsonConvert.DeserializeObject<StudentSignupRequestCommand>(registrationRequest.CommandJson)!;
            else throw new InvalidRequestArgumentException("Registration Request contains unknown UserType.");

            var newBaseUser = await SaveGetNewBaseUserAsync(signupRequestCommand);

            await SaveSpecificUserTypeEntityAsync(
                signupRequestCommand,
                newBaseUser.ItemId,
                registrationRequest.UserType);

            var approverTokenUser = _jwtAuthenticationService.GetAuthTokenUser();

            var approverDesignation = await _designationReadRepository.GetDesignationByUserIdAsync(approverTokenUser.BaseUserId) ?? throw new ItemNotFoundException($"Designation for BaseUserId {approverTokenUser.BaseUserId} not found in the database.");

            await _universityEmailService.SendSignupVerificationAsync(
                fromTokenUser: approverTokenUser,
                toBaseUser: newBaseUser,
                senderDesignation: approverDesignation.Title);
        }

        public async Task RequestSignupAsync(
            AbstractSignupRequestCommand command,
            UserType userType)
        {
            var creatorTokenUser = _jwtAuthenticationService.GetAuthTokenUser();
            var creatorDesignation = await _designationReadRepository.GetDesignationByUserIdAsync(creatorTokenUser.BaseUserId)
                ?? throw new ItemNotFoundException($"Designation for BaseUserId {creatorTokenUser.BaseUserId} not found in the database.");

            var request = new RegistrationRequest
            {
                UserType = userType,
                CommandJson = JsonConvert.SerializeObject(command),
                CreatorUserId = creatorTokenUser.BaseUserId,
                CreatorUserRoles = creatorTokenUser.UserRoles ?? Array.Empty<UserRole>(),
                ActionStatus = RegistrationActionStatus.Pending,
                CreatorDesignationId = creatorDesignation.ItemId
            };

            request.InitiateEntityBase();
            await _registrationRequestWriteRepository.SaveAsync(request);
        }

        private async Task<BaseUser> SaveGetNewBaseUserAsync(AbstractSignupRequestCommand signupRequestCommand)
        {
            var baseUser = new BaseUser
            {
                FirstName = signupRequestCommand.FirstName,
                LastName = signupRequestCommand.LastName,
                FatherName = signupRequestCommand.FatherName,
                MotherName = signupRequestCommand.MotherName,
                MobilePhone = signupRequestCommand.MobilePhone,
                DateOfBirth = signupRequestCommand.DateOfBirth,
                UniversityId = signupRequestCommand.UniversityId,
                NationalId = signupRequestCommand.NationalId,
                PassportNo = signupRequestCommand.PassportNo,
                SurName = signupRequestCommand.SurName,
                PermanentAddress = signupRequestCommand.PermanentAddress,
                PresentAddress = signupRequestCommand.PresentAddress,
                AlternatePhone = signupRequestCommand.AlternatePhone,
                PersonalEmail = signupRequestCommand.PersonalEmail,
                UniversityEmail = signupRequestCommand.UniversityEmail,
                Gender = Enum.TryParse<Gender>(signupRequestCommand.Gender, out var gender) ? gender : Gender.Unspecified,
                AccountStatus = AccountStatus.Deactive,
                UserType = UserType.Employee,
            };

            baseUser.InitiateUserWithEntityBase();
            baseUser.AddRole(UserRole.GeneralEmployee, UserRole.Anonymous);
            baseUser.AddRole(signupRequestCommand.AdditionalUserRoles);

            await _baseUserWriteRepository.SaveAsync(baseUser);

            return baseUser;
        }

        private Task SaveSpecificUserTypeEntityAsync(
            AbstractSignupRequestCommand signupRequestCommand,
            string baseUserId,
            UserType userType)
            => (
                _specificSignupServices
                .FirstOrDefault(service => service.UserType == userType)
                    ?? throw new SpecificSignupServiceNotFoundException(userType)
            ).CreateAsync(baseUserId, signupRequestCommand);
    }
}
