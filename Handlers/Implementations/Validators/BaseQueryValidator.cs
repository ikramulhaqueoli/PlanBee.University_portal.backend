using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.Validators;

public class BaseQueryValidator<TQuery>
    : IQueryValidator<TQuery> where TQuery : AbstractQuery
{
    public virtual ValidationResponse Validate(TQuery command)
    {
        return new ValidationResponse();
    }
}