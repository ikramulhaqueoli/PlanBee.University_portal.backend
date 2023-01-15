using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Handlers.Responses;

namespace PlanBee.University_portal.backend.Handlers;

public interface IQueryDispatcher
{
    Task<AbstractResponse> DispatchAsync<TQuery>(TQuery query)
        where TQuery : AbstractQuery;
}