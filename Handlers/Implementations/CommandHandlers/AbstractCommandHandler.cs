using Microsoft.Extensions.Logging;
using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Enums;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.CommandHandlers;

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
                "Successfully handled command: {FullName}",
                command.GetType().FullName);

            return response;
        }
        catch (AbstractBusinessException exception)
        {
            response = new CommandResponse();
            response.SetCommandError(
                ResponseErrorType.BusinessException,
                $"Business Error: {exception.Message}");

            _logger.LogError(
                exception,
                "Business exception thrown while handling command: {FullName}, Message: {ErrorMessage}",
                command.GetType().FullName,
                exception.Message);
        }
        catch (Exception exception)
        {
            response = new CommandResponse();
            response.SetCommandError(
                ResponseErrorType.SystemException,
                "Something went wrong. Unhandled exception thrown.");

            _logger.LogError(
                exception,
                "Unhandled exception thrown while handling command: {FullName}, Message: {ErrorMessage}",
                command.GetType().FullName,
                exception.Message);
        }

        return response;
    }

    public abstract Task<CommandResponse> HandleAsync(TCommand command);
}