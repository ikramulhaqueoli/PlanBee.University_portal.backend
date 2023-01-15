using Newtonsoft.Json;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;
using PlanBee.University_portal.backend.Domain.Enums;

namespace PlanBee.University_portal.backend.Services.Implementations;

public class UserSignupService : IUserSignupService
{
    private readonly IRegistrationRequestWriteRepository _registrationRequestWriteRepository;

    public UserSignupService(IRegistrationRequestWriteRepository registrationRequestWriteRepository)
    {
        _registrationRequestWriteRepository = registrationRequestWriteRepository;
    }

    public async Task SignupAsync(UserSignupCommand command)
    {
        var request = new RegistrationRequest
        {
            EntityType = nameof(BaseUser),
            ModelDataJson = JsonConvert.SerializeObject(command),
            CreatorUserId = null,
            CreatorUserRole = null,
            ActionStatus = RequestActionStatus.None
        };

        await _registrationRequestWriteRepository.SaveAsync(request);
    }
}