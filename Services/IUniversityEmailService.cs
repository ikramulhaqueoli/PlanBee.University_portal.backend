namespace PlanBee.University_portal.backend.Services
{
    public interface IUniversityEmailService
    {
        Task SendSignupVefificationAsync(string baseUserId);
    }
}
