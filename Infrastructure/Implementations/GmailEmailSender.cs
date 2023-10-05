using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using PlanBee.University_portal.backend.Domain.Utils;

namespace PlanBee.University_portal.backend.Infrastructure.Implementations
{
    public class GmailEmailSender : IEmailSender
    {
        private const string GMAIL_SMTP = "smtp.gmail.com";
        private const int SMTP_PORT = 587;

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
                message.From.Add(new MailboxAddress(config.Institute.Title, config.Institute.OfficialEmail));
                message.To.Add(new MailboxAddress(toName, config.Institute.SmtpEmail.Email));
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
                return false;
            }
        }
    }
}
