using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;

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
        var user = new BaseUser
        {
        };
        
        await _baseUserWriteRepository.SaveAsync(user);
    }
}