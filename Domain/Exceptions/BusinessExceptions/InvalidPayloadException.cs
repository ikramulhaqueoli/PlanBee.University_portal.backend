namespace PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions
{
    public class InvalidPayloadException : AbstractBusinessException
    {
        public InvalidPayloadException(string message) : base(message)
        {
        }
    }
}
