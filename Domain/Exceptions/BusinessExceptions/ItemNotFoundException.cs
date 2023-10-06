namespace PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions
{
    public class ItemNotFoundException : AbstractBusinessException
    {
        public ItemNotFoundException(string message) : base(message) { }
    }
}
