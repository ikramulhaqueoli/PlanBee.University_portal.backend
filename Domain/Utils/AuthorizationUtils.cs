using Microsoft.AspNetCore.Mvc.Filters;
using PlanBee.University_portal.backend.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;

namespace PlanBee.University_portal.backend.Domain.Utils
{
    public static class AuthorizationUtils
    {
        private const string ROLE_STR = "role";
        private const string AUTHORIZATION_STR = "Authorization";
        private const string BEARER_STR = "Bearer ";

        public static bool HasRoleInToken(this AuthorizationFilterContext context, params UserRole[] userRoles)
        {
            string authorizationHeader = context.HttpContext.Request.Headers[AUTHORIZATION_STR].FirstOrDefault()!;

            if (string.IsNullOrWhiteSpace(authorizationHeader))
            {
                return false;
            }

            if (authorizationHeader.StartsWith(BEARER_STR, StringComparison.OrdinalIgnoreCase))
            {
                string token = authorizationHeader[BEARER_STR.Length..];

                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenS = tokenHandler.ReadJwtToken(token);
                    var claims = tokenS.Claims;
                    var tokenRoles = claims.Where(c => c.Type == ROLE_STR).Select(c => c.Value).ToList();

                    return userRoles.All(item => tokenRoles.Contains(item.ToString()));
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }
    }
}
