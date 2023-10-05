namespace PlanBee.University_portal.backend.Domain.Models.Employee
{
    public class WorkExperience
    {
        public string InstituteName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}