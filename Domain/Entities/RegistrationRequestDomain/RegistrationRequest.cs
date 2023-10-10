using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using System.Text.Json.Serialization;

namespace PlanBee.University_portal.backend.Domain.Entities.RegistrationRequestDomain;

public class RegistrationRequest : EntityBase
{
    [BsonRepresentation(BsonType.String)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserType UserType { get; set; }

    [JsonIgnore]
    public string CommandJson { get; set; } = null!;

    public string CreatorUserId { get; set; } = null!;

    [BsonRepresentation(BsonType.String)]
    [JsonConverter(typeof(EnumToStringArrayConverter<UserRole>))]
    public UserRole[] CreatorUserRoles { get; set; } = null!;

    public string CreatorDesignationId { get; set; } = null!;

    [BsonRepresentation(BsonType.String)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RegistrationActionStatus ActionStatus { get; set; }

    public List<ReviewLog> ReviewLogs { get; set; } = new List<ReviewLog>();

    public void Approve(string actionComment, string reviewerUserId)
    {
        AddReviewLog(actionComment, reviewerUserId);
        ActionStatus = RegistrationActionStatus.Approved;
    }

    public void KeepPending(string actionComment, string reviewerUserId)
    {
        AddReviewLog(actionComment, reviewerUserId);
        ActionStatus = RegistrationActionStatus.Pending;
    }

    public void Reject(string actionComment, string reviewerUserId)
    {
        AddReviewLog(actionComment, reviewerUserId);
        ActionStatus = RegistrationActionStatus.Rejected;
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