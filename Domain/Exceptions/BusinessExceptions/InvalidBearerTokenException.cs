namespace PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions
{
    public class InvalidBearerTokenException : AbstractBusinessException
    {
        public InvalidBearerTokenException(string message) : base(message) { }
    }
}
