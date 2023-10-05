namespace PlanBee.University_portal.backend.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(
            string toName,
            string toEmail,
            string subject,
            string body);
    }
}
