namespace PlanBee.University_portal.backend.Domain.Models.Employee
{
    public class EducationalQualification
    {
        public string Degree { get; set; } = null!;
        public string Institute { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Result { get; set; } = null!;
        public string ResultOutOf { get; set; } = null!;
    }
}