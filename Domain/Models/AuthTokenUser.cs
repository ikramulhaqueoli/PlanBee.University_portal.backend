using PlanBee.University_portal.backend.Domain.Enums.Business;
using System.Text.Json.Serialization;

namespace PlanBee.University_portal.backend.Domain.Models
{
    public class AuthTokenUser
    {
        public string BaseUserId { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        
        public string LastName { get; set; } = null!;
        
        public string MobilePhone { get; set; } = null!;
        
        public string UniversityId { get; set; } = null!;
        
        public string? SurName { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole[]? UserRoles { get; set; }
        
        public string? AlternatePhone { get; set; }
        
        public string PersonalEmail { get; set; } = null!;
        
        public string? UniversityEmail { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AccountStatus AccountStatus { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserType UserType { get; set; }
    }
}
