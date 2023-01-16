namespace PlanBee.University_portal.backend.Domain.Exceptions.SystemExceptions;

public class QueryHandlerNotFoundException : AbstractSystemException
{
    public QueryHandlerNotFoundException(Type queryType)
    {
        Message = $"No suitable handler found for query: {queryType.FullName}";
    }

    public override string Message { get; }
}