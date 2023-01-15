using System.Net;

namespace PlanBee.University_portal.backend.CommandHandlers.Responses;

public class ValidationResponse : AbstractResponse
{ 
    public IList<ValidationError> ValidationErrors { get; } = new List<ValidationError>();

    public void AddValidationError(string propertyName, string message)
    {
        var error = new ValidationError(propertyName, message);
        ValidationErrors.Add(error);
    }

    public override bool Success => !ValidationErrors.Any();

    public override HttpStatusCode StatusCode => Success
        ? HttpStatusCode.OK
        : HttpStatusCode.BadRequest;
}