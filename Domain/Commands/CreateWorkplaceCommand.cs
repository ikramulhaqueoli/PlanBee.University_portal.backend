using PlanBee.University_portal.backend.Domain.Constants;

namespace PlanBee.University_portal.backend.Domain.Commands;

public class CreateWorkplaceCommand : AbstractCommand
{
    public string WorkplaceType { get; set; } = null!;

    public string WorkplaceTitle { get; set; } = null!;

    public string WorkplaceId { get; set; } = null!;

    public IEnumerable<string> SectionNames { get; set; } = new[] { WorkplaceConstants.MainSectionName };
}