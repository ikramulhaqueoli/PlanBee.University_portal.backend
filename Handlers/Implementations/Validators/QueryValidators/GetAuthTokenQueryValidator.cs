using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.Validators.QueryValidators;

public class GetAuthTokenQueryValidator : BaseQueryValidator<GetAuthTokenQuery>
{
    public override ValidationResponse Validate(GetAuthTokenQuery query)
    {
        var response = base.Validate(query);
        return response;
    }
}