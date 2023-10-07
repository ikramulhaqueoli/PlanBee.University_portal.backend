using PlanBee.University_portal.backend.Domain.Models.Employee;

namespace PlanBee.University_portal.backend.Domain.Commands;

public class EmployeeSignupCommand : AbstractSignupCommandModel
{
    public DateTime JoiningDate { get; set; }
    public string? NationalId { get; set; }
    public string? PassportNo { get; set; }
    public string? UniversityEmail { get; set; }
    public string WorkplaceId { get; set; } = null!;
    public string DesignationId { get; set; } = null!;
    public List<EducationalQualification> EducationalQualifications { get; set; } = new List<EducationalQualification>();
    public List<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
}