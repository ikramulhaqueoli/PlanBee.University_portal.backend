namespace PlanBee.University_portal.backend.Domain.Exceptions;

public class QueryHandlerNotFoundException : AbstractBusinessException
{
    public QueryHandlerNotFoundException(Type queryType)
    {
        Message = $"No suitable handler found for query: {queryType.FullName}";
    }

    public override string Message { get; }
}