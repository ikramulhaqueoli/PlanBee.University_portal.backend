using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Handlers.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.Validators;

public class GetAuthTokenQueryValidator : BaseQueryValidator<GetAuthTokenQuery>
{
    public override ValidationResponse Validate(GetAuthTokenQuery query)
    {
        var response = base.Validate(query);
        return response;
    }
}