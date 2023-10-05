namespace PlanBee.University_portal.backend.Domain.Commands
{
    public class VerifySignupEmailCommand : AbstractCommand
    {
        public string VerificationCode { get; set; } = null!;

        public string NewPassword { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
    }
}
