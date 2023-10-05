namespace PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;

public abstract class AbstractBusinessException : Exception
{
    public AbstractBusinessException(string message) : base(message) { }
}