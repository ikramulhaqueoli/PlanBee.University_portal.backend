using PlanBee.University_portal.backend.Domain.Enums.System;

namespace PlanBee.University_portal.backend.Domain.Exceptions.SystemExceptions;

public class AbstractSystemException : AbstractException
{
    private const string EXCEPTION_MESSAGE_PREFIX = "System Exception: ";

    public AbstractSystemException(string message)
        : base($"{EXCEPTION_MESSAGE_PREFIX}{message}") { }

    public override ResponseErrorType ErrorType => ResponseErrorType.BusinessException;
}