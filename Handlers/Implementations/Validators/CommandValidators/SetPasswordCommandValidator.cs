using PlanBee.University_portal.backend.Domain.Commands;
using PlanBee.University_portal.backend.Domain.Constants;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Responses;
using System.Text.RegularExpressions;

namespace PlanBee.University_portal.backend.Handlers.Implementations.Validators.CommandValidators
{
    public class SetPasswordCommandValidator : BaseCommandValidator<SetPasswordCommand>
    {
        public override ValidationResponse Validate(SetPasswordCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.NewPassword))
            {
                throw new InvalidPayloadException("New Password must not be empty.");
            }

            if (Regex.IsMatch(command.NewPassword, BusinessConstants.STRONG_PASSWORD_REGEX) is false)
            {
                throw new InvalidPayloadException("Password must consist of 12 characters and contain atleast one capital, small, numeric.");
            }

            if (command.NewPassword != command.ConfirmPassword)
            {
                throw new InvalidPayloadException("Confirm Password must match the new password.");
            }

            return new ValidationResponse();
        }
    }
}
