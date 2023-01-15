using PlanBee.University_portal.backend.Domain.Enums;

namespace PlanBee.University_portal.backend.Handlers.Responses;

public class ResponseError
{
    public ResponseError(ResponseErrorType errorType, string message)
    {
        ErrorType = errorType.ToString();
        Message = message;
    }

    public string ErrorType { get; }

    public string Message { get; }
}