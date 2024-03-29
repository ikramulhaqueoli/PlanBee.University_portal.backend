using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers;

public interface IQueryValidator<in TQuery>
    where TQuery : AbstractQuery
{
    QueryResponse TryValidatePrimary(TQuery query);

    void ValidatePrimary(TQuery query);
}