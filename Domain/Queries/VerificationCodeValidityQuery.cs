namespace PlanBee.University_portal.backend.Domain.Queries
{
    public class VerificationCodeValidityQuery : AbstractQuery
    {
        public string VerificationCode { get; set; } = null!;
    }
}
