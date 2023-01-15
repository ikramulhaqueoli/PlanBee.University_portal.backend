namespace PlanBee.University_portal.backend.Domain.Exceptions;

public class CommandHandlerNotFoundException : AbstractBusinessException
{
    public CommandHandlerNotFoundException(Type commandType)
    {
        Message = $"No suitable handler found for command: {commandType.FullName}";
    }

    public override string Message { get; }
}