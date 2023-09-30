using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Enums;

namespace PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

public class RegistrationRequest : EntityBase
{
    public UserType UserType { get; set; }

    public string ModelDataJson { get; set; } = null!;

    public string CreatorUserId { get; set; } = null!;

    public string CreatorUserRole { get; set; } = null!;

    public RequestActionStatus ActionStatus { get; set; }

    public List<ReviewLog> ReviewLogs { get; set; } = new List<ReviewLog>();

    public void Approve(string actionComment, string reviewerUserId)
    {
        AddReviewLog(actionComment, reviewerUserId);
        ActionStatus = RequestActionStatus.Approved;
    }

    private void AddReviewLog(string actionComment, string reviewerUserId)
    {
        var reviewLog = new ReviewLog
        {
            ActionComment = actionComment,
            LastReviewedAt = DateTime.Now,
            ReviewerUserId = reviewerUserId,
        };

        ReviewLogs ??= new List<ReviewLog>();
        ReviewLogs.Add(reviewLog);
    }
}