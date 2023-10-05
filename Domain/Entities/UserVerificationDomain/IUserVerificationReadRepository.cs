namespace PlanBee.University_portal.backend.Domain.Entities.UserVerificationDomain
{
    public interface IUserVerificationReadRepository
    {
        Task<UserVerification?> GetByVerificationCodeAsync(string verificationCode);
    }
}
