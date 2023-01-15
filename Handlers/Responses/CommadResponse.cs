using System.Net;
using PlanBee.University_portal.backend.Domain.Enums;

namespace PlanBee.University_portal.backend.Handlers.Responses;

public class CommandResponse : AbstractResponse
{
    public ResponseError? CommandError { get; private set; }

    public override bool Success => CommandError is { };

    public override HttpStatusCode StatusCode => Success
        ? HttpStatusCode.OK
        : HttpStatusCode.BadRequest;

    public void SetCommandError(ResponseErrorType errorType, string message)
    {
        CommandError = new ResponseError(errorType, message);
    }
}