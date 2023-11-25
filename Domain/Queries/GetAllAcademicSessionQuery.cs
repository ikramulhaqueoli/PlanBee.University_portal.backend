namespace PlanBee.University_portal.backend.Domain.Queries
{
    public class GetAllAcademicSessionQuery : AbstractQuery
    {
        public bool ActiveOnly { get; set; } = true;
    }
}
