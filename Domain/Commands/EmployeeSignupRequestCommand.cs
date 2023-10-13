using NSwag.Annotations;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Models.Employee;
using System.Text.Json.Serialization;

namespace PlanBee.University_portal.backend.Domain.Commands;

public class EmployeeSignupRequestCommand : AbstractSignupRequestCommand
{
    public DateTime JoiningDate { get; set; }
    public string WorkplaceId { get; set; } = null!;
    public string DesignationId { get; set; } = null!;
    [OpenApiIgnore]
    public string? DesignationTitle { get; set; }
    [OpenApiIgnore]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DesignationType? DesignationType { get; set; }
    public List<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
}