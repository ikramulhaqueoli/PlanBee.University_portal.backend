namespace PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions
{
    public class InvalidRequestArgumentException : AbstractBusinessException
    {
        public InvalidRequestArgumentException(string message) : base(message)
        {
        }
    }
}
