using PlanBee.University_portal.backend.Domain.Enums.Business;

namespace PlanBee.University_portal.backend.Domain.Commands
{
    public class RegistrationActionCommand : AbstractCommand
    {
        public string RegistrationRequestId { get; set; } = null!;

        public RegistrationActionStatus ActionStatus { get; set; }

        public string ActionComment { get; set; } = string.Empty;
    }
}
