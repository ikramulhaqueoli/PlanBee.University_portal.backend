namespace PlanBee.University_portal.backend.Domain.Exceptions.SystemExceptions;

public class QueryHandlerNotFoundException : AbstractSystemException
{
    public QueryHandlerNotFoundException(Type queryType)
        : base($"No suitable handler found for query: {queryType.FullName}") { }
}