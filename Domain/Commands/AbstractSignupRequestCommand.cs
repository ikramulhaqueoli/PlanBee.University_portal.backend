using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Models.Employee;
using System.Text.Json.Serialization;

namespace PlanBee.University_portal.backend.Domain.Commands
{
    public abstract class AbstractSignupRequestCommand : AbstractCommand
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? SurName { get; set; }
        public string FatherName { get; set; } = null!;
        public string MotherName { get; set; } = null!;
        public string MobilePhone { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string PermanentAddress { get; set; } = null!;
        public string PresentAddress { get; set; } = null!;
        public string? AlternatePhone { get; set; }
        public string PersonalEmail { get; set; } = null!;
        public string? UniversityEmail { get; set; }
        public string Gender { get; set; } = null!;
        [JsonConverter(typeof(EnumToStringArrayConverter<UserRole>))]
        public UserRole[]? AdditionalUserRoles { get; set; }
        public string UniversityId { get; set; } = null!;
        public string? NationalId { get; set; }
        public string? PassportNo { get; set; }
        public string? BirthCertificateNo { get; set; }
        public List<EducationalQualification> EducationalQualifications { get; set; } = new List<EducationalQualification>();
    }
}
