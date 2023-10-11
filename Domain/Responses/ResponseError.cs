using PlanBee.University_portal.backend.Domain.Enums.System;
using PlanBee.University_portal.backend.Domain.Exceptions;

namespace PlanBee.University_portal.backend.Domain.Responses;

public class ResponseError
{
    private const string UNHANDLED_EXCEPTION_MESSAGE = "Something went wrong. Unhandled exception thrown.";

    public ResponseError(AbstractException exception)
    {
        ErrorType = exception.ErrorType.ToString();
        ErrorName = exception.GetType().Name;
        Message = exception.Message;
    }

    public ResponseError(Exception exception)
    {
        ErrorType = ResponseErrorType.UnhandledException.ToString();
        ErrorName = exception.GetType().Name;
        Message = UNHANDLED_EXCEPTION_MESSAGE;
    }

    public string ErrorType { get; }

    public string ErrorName { get; }

    public string Message { get; }
}