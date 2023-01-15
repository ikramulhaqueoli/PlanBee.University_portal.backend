using System.Net;
using PlanBee.University_portal.backend.Domain.Enums;

namespace PlanBee.University_portal.backend.CommandHandlers.Responses;

public class CommandResponse : AbstractResponse
{
    public CommandError? CommandError { get; private set; }

    public void SetCommandError(CommandErrorType errorType, string message)
    {
        CommandError = new CommandError(errorType, message);
    }

    public override bool Success => CommandError is { };

    public override HttpStatusCode StatusCode => Success
        ? HttpStatusCode.OK
        : HttpStatusCode.BadRequest;
}