using Microsoft.Extensions.DependencyInjection;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _service;

    public QueryDispatcher(IServiceProvider service)
    {
        _service = service;
    }

    public async Task<AbstractResponse> DispatchAsync<TQuery>(TQuery query)
        where TQuery : AbstractQuery
    {
        var validator = _service.GetService<IQueryValidator<TQuery>>();
        var validationResponse = validator!.TryValidatePrimary(query);
        if (validationResponse.Success is false)
        {
            return validationResponse;
        }

        var handler = _service.GetService<IQueryHandler<TQuery>>();
        return await handler!.TryHandleAsync(query);
    }
}