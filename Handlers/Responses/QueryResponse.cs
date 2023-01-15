using System.Net;
using PlanBee.University_portal.backend.Domain.Enums;

namespace PlanBee.University_portal.backend.Handlers.Responses;

public class QueryResponse : AbstractResponse
{
    public object? QueryData { get; set; } = null;

    public ResponseError? QueryError { get; private set; }

    public override bool Success => QueryError is { };

    public override HttpStatusCode StatusCode => Success
        ? HttpStatusCode.OK
        : HttpStatusCode.BadRequest;

    public void SetQueryError(ResponseErrorType errorType, string message)
    {
        QueryError = new ResponseError(errorType, message);
    }
}