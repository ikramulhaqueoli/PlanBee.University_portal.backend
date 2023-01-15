using PlanBee.University_portal.backend.Domain.Commands;

namespace PlanBee.University_portal.backend.Domain.Exceptions;

public class HandlerNotFoundException : AbstractBusinessException
{
    public HandlerNotFoundException(Type commandType)
    {
        Message = $"No suitable handler found for command: {commandType.FullName}";
    }

    public override string Message { get; }
}