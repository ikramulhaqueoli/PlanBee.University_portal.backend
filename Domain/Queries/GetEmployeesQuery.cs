namespace PlanBee.University_portal.backend.Domain.Queries
{
    public class GetEmployeesQuery : AbstractQuery
    {
        public string[]? SpecificBaseUserIds { get; set; }
    }
}
