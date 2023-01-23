namespace PlanBee.University_portal.backend.Domain.Commands;

public class EmployeeSignupCommand : AbstractCommand
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string SurName { get; set; } = null!;
    public string MobilePhone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string RegistrationId { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string UserRole { get; set; } = null!;
    public string? UniversityEmail { get; set; }
}