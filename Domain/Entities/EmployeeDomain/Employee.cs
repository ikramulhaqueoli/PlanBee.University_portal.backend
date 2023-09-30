using PlanBee.University_portal.backend.Domain.Models.Employee;

namespace PlanBee.University_portal.backend.Domain.Entities.EmployeeDomain
{
    public class Employee : EntityBase
    {
        public DateTime JoiningDate { get; set; }

        public string WorkplaceId { get; set; } = null!;

        public string DesignationId { get; set; } = null!;

        public string BaseUserId { get; set; } = null!;

        public List<EducationalQualification> EducationalQualifications { get; set; } = new List<EducationalQualification>();

        public List<WordExperience> WordExperiences { get; set; } = new List<WordExperience>();
    }
}
