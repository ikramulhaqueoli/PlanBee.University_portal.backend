using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers;

public interface IQueryHandler<in TQuery>
    where TQuery : AbstractQuery
{
    Task<QueryResponse> TryHandleAsync(TQuery query);

    protected Task<QueryResponse> HandleAsync(TQuery query);
}