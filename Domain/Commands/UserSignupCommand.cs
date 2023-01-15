namespace PlanBee.University_portal.backend.Domain.Commands;

public class UserSignupCommand : AbstractCommand
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? NickName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Gender { get; set; }
    public string? RegistrationId { get; set; }
    public string? PasswordHash { get; set; }
    public string? DateOfBirth { get; set; }
    public string? UserRole { get; set; }
    public string? UniversityEmail { get; set; }
}