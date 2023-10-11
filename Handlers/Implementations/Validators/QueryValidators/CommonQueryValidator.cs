using PlanBee.University_portal.backend.Domain.Queries;

namespace PlanBee.University_portal.backend.Handlers.Implementations.Validators.QueryValidators;

public class CommonQueryValidator<TQuery>
    : AbstractQueryValidator<TQuery> where TQuery : AbstractQuery
{
    public override void ValidatePrimary(TQuery query)
    {
    }
}