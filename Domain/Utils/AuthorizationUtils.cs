using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using PlanBee.University_portal.backend.Domain.Constants;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PlanBee.University_portal.backend.Domain.Utils
{
    public static class AuthorizationUtils
    {
        private const string ROLE_STR = "role";
        private const string AUTHORIZATION_STR = "Authorization";
        private const string BEARER_STR = "Bearer ";

        public static bool HasRoleInToken(this AuthorizationFilterContext context, params UserRole[] userRoles)
        {
            var token = context.GetAuthTokenString();
            var authTokenUser = ToAuthTokenUser(token);
            var authUserRoles = authTokenUser?.UserRoles?.ToList();

            if (authUserRoles == null) return false;
            return userRoles.All(item => authUserRoles.Contains(item));
        }

        public static string GetAuthTokenString(this AuthorizationFilterContext context)
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

        public static ClaimsIdentity ToClaimsIdentity(this BaseUser baseUser)
        {
            var claims = new List<Claim>
            {
                new(nameof(AuthTokenUser.BaseUserId), baseUser.ItemId),
                new(nameof(AuthTokenUser.FirstName), baseUser.FirstName),
                new(nameof(AuthTokenUser.LastName), baseUser.LastName),
                new(nameof(AuthTokenUser.MobilePhone), baseUser.MobilePhone),
                new(nameof(AuthTokenUser.UniversityId), baseUser.UniversityId),
                new(nameof(AuthTokenUser.SurName), baseUser.SurName ?? string.Empty),
                new(nameof(AuthTokenUser.Gender), baseUser.Gender.ToString()),
                new(nameof(AuthTokenUser.AlternatePhone), baseUser.AlternatePhone ?? string.Empty),
                new(nameof(AuthTokenUser.PersonalEmail), baseUser.PersonalEmail),
                new(nameof(AuthTokenUser.UniversityEmail), baseUser.UniversityEmail ?? string.Empty),
                new(nameof(AuthTokenUser.AccountStatus), baseUser.AccountStatus.ToString()),
                new(nameof(AuthTokenUser.UserType), baseUser.UserType.ToString())
            };

            var roleClaims = baseUser.UserRoles?
            .Select(role => new Claim(ClaimTypes.Role, role.ToString()))
            .ToList() ?? new List<Claim>();

            claims.AddRange(roleClaims);

            return new ClaimsIdentity(claims);
        }

        public static AuthTokenUser ToAuthTokenUser(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenS = tokenHandler.ReadJwtToken(authToken);

            var authTokenUser = new AuthTokenUser();

            var claims = tokenS.Claims;
            foreach (var claim in tokenS.Claims)
            {
                switch (claim.Type)
                {
                    case nameof(AuthTokenUser.BaseUserId):
                        authTokenUser.BaseUserId = claim.Value;
                        break;
                    case nameof(AuthTokenUser.FirstName):
                        authTokenUser.FirstName = claim.Value;
                        break;
                    case nameof(AuthTokenUser.LastName):
                        authTokenUser.LastName = claim.Value;
                        break;
                    case nameof(AuthTokenUser.MobilePhone):
                        authTokenUser.MobilePhone = claim.Value;
                        break;
                    case nameof(AuthTokenUser.UniversityId):
                        authTokenUser.UniversityId = claim.Value;
                        break;
                    case nameof(AuthTokenUser.SurName):
                        authTokenUser.SurName = claim.Value;
                        break;
                    case nameof(AuthTokenUser.Gender):
                        if (Enum.TryParse(claim.Value, out Gender gender)) authTokenUser.Gender = gender;
                        break;
                    case nameof(AuthTokenUser.AccountStatus):
                        if (Enum.TryParse(claim.Value, out AccountStatus accountStatus)) authTokenUser.AccountStatus = accountStatus;
                        break;
                    case nameof(AuthTokenUser.UserType):
                        if (Enum.TryParse(claim.Value, out UserType userType)) authTokenUser.UserType = userType;
                        break;
                }
            }

            authTokenUser.UserRoles = claims
                .Where(claim => claim.Type == ROLE_STR)
                .Select(claim => Enum.TryParse(claim.Value, out UserRole role) ? role : UserRole.None)
                .Where(role => role != UserRole.None)
                .Distinct().ToArray();

            return authTokenUser;
        }
    }
}
