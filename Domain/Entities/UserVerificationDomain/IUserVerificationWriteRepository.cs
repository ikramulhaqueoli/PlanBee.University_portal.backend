using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Domain.Entities.UserVerificationDomain
{
    public interface IUserVerificationWriteRepository
    {
        Task SaveAsync(UserVerification userVerification);

        Task DeleteAllAsync(string baseUserId, UserVerificationType verificationType);
    }
}
