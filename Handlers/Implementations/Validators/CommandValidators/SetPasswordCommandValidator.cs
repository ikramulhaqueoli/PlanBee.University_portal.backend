using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Constants;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using System.Text.RegularExpressions;

namespace PlanBee.University_portal.backend.Handlers.Implementations.Validators.CommandValidators
{
    public class SetPasswordCommandValidator : AbstractCommandValidator<SetPasswordCommand>
    {
        public override void ValidatePrimary(SetPasswordCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.NewPassword))
            {
                throw new InvalidRequestArgumentException("New Password must not be empty.");
            }

            if (Regex.IsMatch(command.NewPassword, RegexConstants.PASSWORD_REGEX) is false)
            {
                throw new InvalidRequestArgumentException("Password must consist of 10 characters and contain atleast one capital, small, numeric.");
            }

            if (command.NewPassword != command.ConfirmPassword)
            {
                throw new InvalidRequestArgumentException("Confirm Password must match the new password.");
            }
        }
    }
}
