using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PlanBee.University_portal.backend.Domain.Constants;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Queries;
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
            var token = context.GetAuthToken();
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenS = tokenHandler.ReadJwtToken(token);
            var claims = tokenS.Claims;
            var tokenRoles = claims.Where(c => c.Type == ROLE_STR).Select(c => c.Value).ToList();

            return userRoles.All(item => tokenRoles.Contains(item.ToString()));
        }

        public static string GetAuthToken(this AuthorizationFilterContext context)
        {
            string authorizationHeader = context.HttpContext.Request.Headers[AUTHORIZATION_STR].FirstOrDefault()!;

            if (string.IsNullOrWhiteSpace(authorizationHeader))
            {
                return string.Empty;
            }

            if (authorizationHeader.StartsWith(BEARER_STR, StringComparison.OrdinalIgnoreCase))
            {
                string token = authorizationHeader[BEARER_STR.Length..];
                return token;
            }

            return string.Empty;
        }

        public static string GetBaseUserId(string jwtToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwtToken);

                var claim = token.Claims.FirstOrDefault(c => c.Type == BusinessConstants.USER_ID_KEY);

                if (claim != null)
                {
                    return claim.Value;
                }
            }
            catch (SecurityTokenException ex)
            {
                throw new InvalidBearerTokenException($"Token could not be parsed. Reason: {ex.Message}");
            }

            throw new InvalidBearerTokenException($"{BusinessConstants.USER_ID_KEY} not found in token");
        }
    }
}
