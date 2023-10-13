using PlanBee.University_portal.backend.Domain.Enums.Business;
using System.Text.Json.Serialization;

namespace PlanBee.University_portal.backend.Domain.Commands;

public class StudentSignupRequestCommand : AbstractSignupRequestCommand
{
    public DateTime AdmissionDate { get; set; }

    public string DepartmentId { get; set; } = null!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RecidenceStatus RecidenceStatus { get; set; }
}
