using PlanBee.University_portal.backend.Domain.Enums.System;

namespace PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;

public abstract class AbstractBusinessException : AbstractException
{
    private const string EXCEPTION_MESSAGE_PREFIX = "Business Exception: ";

    public AbstractBusinessException(string message)
        : base($"{EXCEPTION_MESSAGE_PREFIX}{message}") { }

    public override ResponseErrorType ErrorType => ResponseErrorType.BusinessException;
}