using System.Net;

namespace PlanBee.University_portal.backend.CommandHandlers.Responses;

public abstract class AbstractResponse
{
    public abstract bool Success { get; }
    
    public abstract HttpStatusCode StatusCode { get; }
}