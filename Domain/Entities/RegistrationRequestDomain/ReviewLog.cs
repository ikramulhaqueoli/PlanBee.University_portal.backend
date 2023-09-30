namespace PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain
{
    public class ReviewLog
    {
        public string? ReviewerUserId { get; set; }
        public DateTime? LastReviewedAt { get; set; }

        public string? ActionComment { get; set; }
    }
}
