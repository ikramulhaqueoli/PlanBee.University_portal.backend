using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using PlanBee.University_portal.backend.Domain.Utils;
using Microsoft.Extensions.Logging;

namespace PlanBee.University_portal.backend.Infrastructure.Implementations
{
    public class GmailEmailSender : IEmailSender
    {
        private const string GMAIL_SMTP = "smtp.gmail.com";
        private const int SMTP_PORT = 587;

        private readonly ILogger<GmailEmailSender> _logger;

        public GmailEmailSender(ILogger<GmailEmailSender> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(
            string toName,
            string toEmail,
            string subject,
            string body)
        {
            var config = AppConfigUtil.Config;

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(config.Institute.Title, config.Institute.SmtpEmail.Email));
                message.To.Add(new MailboxAddress(toName, toEmail));
                message.Subject = subject;
                message.Body = new BodyBuilder
                {
                    HtmlBody = body
                }.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(GMAIL_SMTP, SMTP_PORT, SecureSocketOptions.StartTls);

                await client.AuthenticateAsync(
                    config.Institute.SmtpEmail.Email,
                    config.Institute.SmtpEmail.AppPassword);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Mail Sending failed. Reason: {ex.Message}");
                return false;
            }
        }
    }
}
