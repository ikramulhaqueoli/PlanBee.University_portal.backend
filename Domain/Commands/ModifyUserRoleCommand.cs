using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Domain.Commands
{
    public class ModifyUserRoleCommand : AbstractCommand
    {
        public string TargetBaseUserId { get; set; } = null!;

        public UserRole[]? RolesToBeRemoved { get; set; }

        public UserRole[]? RolesToBeAdded { get; set; }
    }
}
