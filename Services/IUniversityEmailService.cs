using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Models;

namespace PlanBee.University_portal.backend.Services
{
    public interface IUniversityEmailService
    {
        Task SendSignupVerificationAsync(
            AuthTokenUser fromTokenUser,
            BaseUser toBaseUser,
            string senderDesignation);
    }
}
