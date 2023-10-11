using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Queries;

namespace PlanBee.University_portal.backend.Handlers.Implementations.Validators.QueryValidators;

public class GetAuthTokenQueryValidator : AbstractQueryValidator<GetAuthTokenQuery>
{
    public override void ValidatePrimary(GetAuthTokenQuery query)
    {
        if (string.IsNullOrWhiteSpace(query.EmailOrUniversityId))
        {
            throw new InvalidRequestArgumentException("Email Address or University Id must not be empty.");
        }
    }
}