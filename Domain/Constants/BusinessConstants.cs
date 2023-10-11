﻿namespace PlanBee.University_portal.backend.Domain.Constants
{
    public class BusinessConstants
    {
        public const string USER_ID_KEY = "BaseUserId";
        public const string MAIN_SECTION = "Main";
        public const int VERIFI_CODE_VALIDITY_DAYS = 7;
        public const string STRONG_PASSWORD_REGEX = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{10,}$";
    }
}
