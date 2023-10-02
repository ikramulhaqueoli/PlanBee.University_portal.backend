using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Enums.System;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Queries;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.QueryHandlers;

public abstract class AbstractQueryHandler<TQuery> : IQueryHandler<TQuery>
    where TQuery : AbstractQuery
{
    private readonly ILogger _logger;

    protected AbstractQueryHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<QueryResponse> TryHandleAsync(TQuery query)
    {
        QueryResponse response;

        try
        {
            response = await HandleAsync(query);

            _logger.LogInformation(
                "Successfully handled command: {FullName}",
                query.GetType().FullName);

            return response;
        }
        catch (AbstractBusinessException exception)
        {
            response = new QueryResponse();
            response.SetQueryError(
                ResponseErrorType.BusinessException,
                $"Business Error: {exception.Message}");

            _logger.LogError(
                exception,
                "Business exception thrown while handling query: {FullName}, Message: {ErrorMessage}",
                query.GetType().FullName,
                exception.Message);
        }
        catch (Exception exception)
        {
            response = new QueryResponse();
            response.SetQueryError(
                ResponseErrorType.SystemException,
                "Something went wrong. Unhandled exception thrown.");

            _logger.LogError(
                exception,
                "Unhandled exception thrown while handling query: {FullName}, Message: {ErrorMessage}",
                query.GetType().FullName,
                exception.Message);
        }

        return response;
    }

    public abstract Task<QueryResponse> HandleAsync(TQuery query);
}