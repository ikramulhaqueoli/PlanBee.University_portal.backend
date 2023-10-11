namespace PlanBee.University_portal.backend.Domain.Constants
{
    public static class RegexConstants
    {
        public const string PASSWORD_REGEX = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{10,}$";

        public const string EMAIL_REGEX = @"^[\w\.-]+@[\w\.-]+\.\w+";
    }
}
