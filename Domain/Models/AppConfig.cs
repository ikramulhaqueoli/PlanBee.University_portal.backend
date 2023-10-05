namespace PlanBee.University_portal.backend.Domain.Models
{

    public class AppConfig
    {
        public Logging Logging { get; set; } = null!;

        public MongoDatabase MongoDatabase { get; set; } = null!;
        
        public Institute Institute { get; set; } = null!;

        public Domain Domain { get; set; } = null!;

        public Jwt Jwt { get; set; } = null!;

        public string AllowedHosts { get; set; } = null!;
    }

    public class Jwt
    {
        public string Key { get; set; } = null!;

        public string Issuer { get; set; } = null!;

        public string Audience { get; set; } = null!;

        public long ExpireTimeout { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; } = null!;
    }

    public class LogLevel
    {
        public string Default { get; set; } = null!;

        public string MicrosoftAspNetCore { get; set; } = null!;
    }

    public class MongoDatabase
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;
    }

    public class Institute
    {
        public string Title { get; set; } = null!;

        public SmtpEmail SmtpEmail { get; set; } = null!;

        public string OfficialEmail { get; set; } = null!;

        public string HelpEmail { get; set; } = null!;

        public string HelpPhone { get; set; } = null!;
    }

    public class Domain
    {
        public string Protocol { get; set; } = null!;

        public string Url { get; set; } = null!;
    }

    public class SmtpEmail
    {
        public string Email { get; set; } = null!;

        public string AppPassword { get; set; } = null!;
    }
}
