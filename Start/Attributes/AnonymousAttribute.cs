using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Utils;

namespace PlanBee.University_portal.backend.Start.Attributes
{
    public class AnonymousAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HasRoleInToken(UserRole.Anonymous) is false)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
