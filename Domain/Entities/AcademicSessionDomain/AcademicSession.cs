namespace PlanBee.University_portal.backend.Domain.Entities.AcademicSessionDomain
{
    public class AcademicSession : EntityBase
    {
        public int YearOne { get; set; }

        public int YearTwo { get; set; }

        public int? YearlySemester { get; set; }
    }
}
