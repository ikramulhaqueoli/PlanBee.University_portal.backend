namespace PlanBee.University_portal.backend.Domain.Commands.Administrative
{
    public class CreateAcademicSessionCommand : AbstractCommand
    {
        public string Title { get; set; } = null!;
    }
}
