namespace PlanBee.University_portal.backend.Services
{
    public interface IUserVerificationService
    {
        Task<bool> VerifyFromEmailAsync(string verificationCode, string password);
    }
}
