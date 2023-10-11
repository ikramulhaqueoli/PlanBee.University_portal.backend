using PlanBee.University_portal.backend.Domain.Enums.System;

namespace PlanBee.University_portal.backend.Domain.Exceptions
{
    public abstract class AbstractException : Exception
    {
        public AbstractException(string message) : base(message) { }

        public abstract ResponseErrorType ErrorType { get; }
    }
}
