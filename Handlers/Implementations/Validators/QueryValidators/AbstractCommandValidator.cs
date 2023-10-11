using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.Validators.QueryValidators;

public abstract class AbstractQueryValidator<TQuery>
    : IQueryValidator<TQuery> where TQuery : AbstractQuery
{
    public QueryResponse TryValidatePrimary(TQuery query)
    {
        QueryResponse response = new();

        try
        {
            ValidatePrimary(query);
            return response;
        }
        catch (InvalidRequestArgumentException exception)
        {
            response.SetQueryError(exception);
        }
        catch (Exception exception)
        {
            response.SetQueryError(exception);
        }

        return response;
    }

    public abstract void ValidatePrimary(TQuery query);
}