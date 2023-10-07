using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

public class RegistrationRequest : EntityBase
{
    [BsonRepresentation(BsonType.String)]
    public UserType UserType { get; set; }

    public object ModelData { get; set; } = null!;

    public string CreatorUserId { get; set; } = null!;

    public string CreatorUserRole { get; set; } = null!;

    [BsonRepresentation(BsonType.String)]
    public RegistrationActionStatus ActionStatus { get; set; }

    public List<ReviewLog> ReviewLogs { get; set; } = new List<ReviewLog>();

    public void Approve(string actionComment, string reviewerUserId)
    {
        AddReviewLog(actionComment, reviewerUserId);
        ActionStatus = RegistrationActionStatus.Approved;
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