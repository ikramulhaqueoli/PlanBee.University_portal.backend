using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Responses;

namespace PlanBee.University_portal.backend.Handlers.Implementations.Validators.CommandValidators;

public abstract class AbstractCommandValidator<TCommand>
    : ICommandValidator<TCommand> where TCommand : AbstractCommand
{
    public async Task<CommandResponse> TryValidateBusinessAsync(TCommand command)
    {
        CommandResponse response = new();

        try
        {
            await ValidateBusinessAsync(command);
            return response;
        }
        catch (InvalidRequestArgumentException exception)
        {
            response.SetCommandError(exception);
        }
        catch (Exception exception)
        {
            response.SetCommandError(exception);
        }

        return response;
    }

    public CommandResponse TryValidatePrimary(TCommand command)
    {
        CommandResponse response = new();

        try
        {
            if (command == null)
            {
                throw new InvalidRequestArgumentException("Request Argument can not be null.");
            }

            ValidatePrimary(command);
            return response;
        }
        catch (InvalidRequestArgumentException exception)
        {
            response.SetCommandError(exception);
        }
        catch (Exception exception)
        {
            response.SetCommandError(exception);
        }

        return response;
    }

    public virtual Task ValidateBusinessAsync(TCommand command) { return Task.CompletedTask; }

    public abstract void ValidatePrimary(TCommand command);
}