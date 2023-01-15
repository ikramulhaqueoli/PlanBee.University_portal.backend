using PlanBee.University_portal.backend.Domain.Enums;

namespace PlanBee.University_portal.backend.CommandHandlers.Responses;

public class CommandError
{
    public CommandError(CommandErrorType errorType, string message)
    {
        ErrorType = errorType.ToString();
        Message = message;
    }

    public string ErrorType { get; }
    
    public string Message { get; }
}