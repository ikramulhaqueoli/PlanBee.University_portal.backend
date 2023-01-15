using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Enums;
using PlanBee.University_portal.backend.Domain.Libraries;

namespace PlanBee.University_portal.backend.Services.Implementations;

public class UserSignupService : IUserSignupService
{
    private readonly IBaseUserWriteRepository _baseUserWriteRepository;

    public UserSignupService(IBaseUserWriteRepository baseUserWriteRepository)
    {
        _baseUserWriteRepository = baseUserWriteRepository;
    }

    public async Task SignupAsync(UserSignupCommand command)
    {
        var passwordHash = command.Password.Md5Hash();
        var user = new BaseUser
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            NickName = command.NickName,
            Phone = command.Phone,
            Email = command.Email,
            Gender = Enum.Parse<Gender>(command.Gender ?? Gender.Unspecified.ToString()),
            RegistrationId = command.RegistrationId,
            PasswordHash = passwordHash,
            DateOfBirth = command.DateOfBirth,
            UserRole = Enum.Parse<UserRoles>(command.UserRole ?? UserRoles.Unknown.ToString()),
            UniversityEmail = command.UniversityEmail,
        };
        
        user.Initiate();
        
        await _baseUserWriteRepository.SaveAsync(user);
    }
}