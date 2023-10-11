using System.Net;

namespace PlanBee.University_portal.backend.Domain.Responses;

public class CommandResponse : AbstractResponse
{
    public ResponseError? CommandError { get; private set; }

    public override bool Success => CommandError is null;

    public override HttpStatusCode StatusCode => Success
        ? HttpStatusCode.OK
        : HttpStatusCode.BadRequest;

    public void SetCommandError<TException>(TException exception)
        where TException : Exception
    {
        CommandError = ResponseError.GetError(exception);
    }
}