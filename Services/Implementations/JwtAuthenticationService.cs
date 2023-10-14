using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Exceptions.BusinessExceptions;
using PlanBee.University_portal.backend.Domain.Models;
using PlanBee.University_portal.backend.Domain.Utils;

namespace PlanBee.University_portal.backend.Services.Implementations;

public class JwtAuthenticationService : IJwtAuthenticationService
{
    private readonly IBaseUserReadRepository _baseUserReadRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtAuthenticationService(
        IBaseUserReadRepository baseUserReadRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _baseUserReadRepository = baseUserReadRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthTokenResponse?> GetAuthTokenAsync(
        string emailOrUniversityId,
        string password)
    {
        var passwordHash = password.Md5Hash();
        var baseUser = await _baseUserReadRepository
            .GetByCredentialsAsync(emailOrUniversityId, passwordHash);
        if (baseUser == null) return null;

        var tokenKey = Encoding.UTF8.GetBytes(AppConfigUtil.Config.Jwt.Key!);
        var timeOut = AppConfigUtil.Config.Jwt.ExpireTimeout;
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = baseUser.ToClaimsIdentity(),
            Expires = DateTime.UtcNow.AddMinutes(timeOut),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new AuthTokenResponse { Token = tokenHandler.WriteToken(token) };
    }

    public AuthTokenUser GetAuthTokenUser()
    {
        var tokenStr = _httpContextAccessor.HttpContext?.Request.Headers.GetAuthTokenString();
        var tokenUser = AuthorizationUtils.ToAuthTokenUser(tokenStr);
        return tokenUser == null 
            ? throw new ItemAlreadyExistsException("No valid user found from the Authorization token.")
            : tokenUser;
    }
}