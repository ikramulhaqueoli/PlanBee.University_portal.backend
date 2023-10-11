using System.Net;

namespace PlanBee.University_portal.backend.Domain.Responses;

public class QueryResponse : AbstractResponse
{
    public object? QueryData { get; set; } = null;

    public ResponseError? QueryError { get; private set; }

    public override bool Success => QueryError is null;

    public override HttpStatusCode StatusCode => Success
        ? HttpStatusCode.OK
        : HttpStatusCode.BadRequest;

    public void SetQueryError<TException>(TException exception)
        where TException : Exception
    {
        QueryError = ResponseError.GetError(exception);
    }
}