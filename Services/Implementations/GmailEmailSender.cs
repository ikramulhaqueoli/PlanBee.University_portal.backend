using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace PlanBee.University_portal.backend.Services.Implementations
{
    public class GmailEmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Softbee Corporation", "softbee.corporation@gmail.com")); // Your Gmail address
                message.To.Add(new MailboxAddress("Ikramul Haque Chowdhury", toEmail));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = body;
                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("softbee.corporation@gmail.com", "amiami!!"); // Your Gmail password or an app password
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Handle exceptions here
            }
        }
    }
}
