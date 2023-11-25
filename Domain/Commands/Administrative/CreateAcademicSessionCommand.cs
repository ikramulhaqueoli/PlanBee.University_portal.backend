namespace PlanBee.University_portal.backend.Domain.Commands.Administrative
{
    public class CreateAcademicSessionCommand : AbstractCommand
    {
        public int YearOne { get; set; }

        public int YearTwo { get; set; }

        public int? YearlySemester { get; set; }
    }
}
