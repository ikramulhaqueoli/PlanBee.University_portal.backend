namespace PlanBee.University_portal.backend.Domain.Utils
{
    public static class UserVerificationUtils
    {
        public static string GetVerificationLink(string verificationCode) => 
            $"{AppConfigUtil.Config.Domain.Protocol}:///{AppConfigUtil.Config.Domain.Url}?verification={verificationCode}";

        public static string GetCodeFromItemId(string itemId) => itemId.Replace("-", "");
    }
}
