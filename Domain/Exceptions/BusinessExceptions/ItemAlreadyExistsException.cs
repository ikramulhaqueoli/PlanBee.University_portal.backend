namespace PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions
{
    public class ItemAlreadyExistsException : AbstractBusinessException
    {
        public ItemAlreadyExistsException(string message) : base(message) { }
    }
}
