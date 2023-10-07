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
    public string ModelDataJson { get; set; } = null!;

    [BsonIgnore]
    public object? ModelData { get; set; }

    public string CreatorUserId { get; set; } = null!;

    public string CreatorUserRole { get; set; } = null!;

    [BsonRepresentation(BsonType.String)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
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