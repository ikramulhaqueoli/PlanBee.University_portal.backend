using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Domain.Queries
{
    public class GetSignupFormDataQuery : AbstractQuery
    {
        public UserType UserType { get; set; }
    }
}
