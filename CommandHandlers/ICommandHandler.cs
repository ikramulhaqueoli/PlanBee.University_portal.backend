using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.CommandHandlers.Responses;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Enums;
using PlanBee.University_portal.backend.Domain.Exceptions;

namespace PlanBee.University_portal.backend.CommandHandlers;

public interface ICommandHandler<in TCommand>
    where TCommand : AbstractCommand
{ 
    Task<CommandResponse> HandleAsync(TCommand command);
}

public abstract class AbstractCommandHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : AbstractCommand
{
    private readonly ILogger _logger;

    protected AbstractCommandHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<CommandResponse> TryHandleAsync(TCommand command)
    {
        CommandResponse response;
        
        try
        {
            response = await HandleAsync(command);
            
            _logger.LogInformation(
                message: "Successfully handled command: {FullName}",
                command.GetType().FullName);

            return response;
        }
        catch (AbstractBusinessException exception)
        {
            response = new CommandResponse();
            response.SetCommandError(
                CommandErrorType.BusinessException,
                message: $"Business Error: {exception.Message}");
            
            _logger.LogError(
                exception,
                message: "Business exception thrown while handling command: {FullName}, Message: {ErrorMessage}",
                command.GetType().FullName, 
                exception.Message);
        }
        catch (Exception exception)
        {
            response = new CommandResponse();
            response.SetCommandError(
                CommandErrorType.SystemException,
                message: "Something went wrong. Unhandled exception thrown.");
            
            _logger.LogError(
                exception,
                message: "Unhandled exception thrown while handling command: {FullName}, Message: {ErrorMessage}",
                command.GetType().FullName, 
                exception.Message);
        }

        return response;
    }

    public abstract Task<CommandResponse> HandleAsync(TCommand command);
}