using PlanBee.University_portal.backend.Domain.Constants;

namespace PlanBee.University_portal.backend.Domain.Commands;

public class CreateDesignationCommand : AbstractCommand
{
    public string WorkplaceCode { get; set; } = null!;

    public string SectionName { get; set; } = WorkplaceConstants.MainSectionName;

    public string DesignationName { get; set; } = null!;
}