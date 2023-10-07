using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Domain.Commands
{
    public abstract class AbstractSignupCommandModel : AbstractCommand
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string SurName { get; set; } = null!;
        public string FatherName { get; set; } = null!;
        public string MotherName { get; set; } = null!;
        public string MobilePhone { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string PermanentAddress { get; set; } = null!;
        public string PresentAddress { get; set; } = null!;
        public string? AlternatePhone { get; set; }
        public string PersonalEmail { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public UserRole[]? AdditionalUserRoles { get; set; } = null!;
        public string UniversityId { get; set; } = null!;
    }
}
