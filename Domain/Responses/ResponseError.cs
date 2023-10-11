using PlanBee.University_portal.backend.Domain.Enums.System;
using PlanBee.University_portal.backend.Domain.Exceptions;

namespace PlanBee.University_portal.backend.Domain.Responses;

public class ResponseError
{
    private const string UNHANDLED_EXCEPTION_MESSAGE = "Something went wrong. Unhandled exception thrown.";

    public static ResponseError GetError<TException>(TException exception) where TException : Exception
    {
        return exception is AbstractException
            ? new ResponseError
            {
                ErrorType = (exception as AbstractException)!.ErrorType.ToString(),
                ErrorName = exception.GetType().Name,
                Message = exception.Message
            }
            : new ResponseError
            {
                ErrorType = ResponseErrorType.UnhandledException.ToString(),
                Message = UNHANDLED_EXCEPTION_MESSAGE
            };
    }

    public string ErrorType { get; private set; } = null!;

    public string? ErrorName { get; private set; }

    public string Message { get; private set; } = null!;
}