namespace PlanBee.University_portal.backend.Domain.Models
{

    public class AppConfig
    {
        public Logging Logging { get; set; } = null!;

        public MongoDatabase MongoDatabase { get; set; } = null!;

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
}
