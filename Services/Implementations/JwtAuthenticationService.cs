using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PlanBee.University_portal.backend.Domain.Constants;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Utils;
using PlanBee.University_portal.backend.Services.Models;

namespace PlanBee.University_portal.backend.Services.Implementations;

public class JwtAuthenticationService : IJwtAuthenticationService
{
    private readonly IBaseUserReadRepository _baseUserReadRepository;

    public JwtAuthenticationService(
        IBaseUserReadRepository baseUserReadRepository)
    {
        _baseUserReadRepository = baseUserReadRepository;
    }

    public async Task<AuthToken?> GetAuthTokenAsync(string universityId, string password)
    {
        var passwordHash = password.Md5Hash();
        var baseUser = await _baseUserReadRepository.GetByCredentialsAsync(universityId, passwordHash);
        if (baseUser == null) return null;

        var tokenKey = Encoding.UTF8.GetBytes(AppConfigUtil.Config.Jwt.Key!);
        var timeOut = AppConfigUtil.Config.Jwt.ExpireTimeout;
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GetClaimsIdentity(baseUser),
            Expires = DateTime.UtcNow.AddMinutes(timeOut),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new AuthToken { Token = tokenHandler.WriteToken(token) };
    }

    private ClaimsIdentity GetClaimsIdentity(BaseUser baseUser)
    {
        var claims = new List<Claim>
        {
            new(BusinessConstants.USER_ID_KEY, baseUser.ItemId.ToString()),
            new(nameof(baseUser.PersonalEmail), baseUser.PersonalEmail ?? string.Empty),
            new(nameof(baseUser.UniversityEmail), baseUser.UniversityEmail ?? string.Empty),
            new(nameof(baseUser.DateOfBirth), baseUser.DateOfBirth.ToString() ?? string.Empty),
            new(nameof(baseUser.FirstName), baseUser.FirstName),
            new(nameof(baseUser.LastName), baseUser.LastName),
            new(nameof(baseUser.Gender), baseUser.Gender.ToString()),
            new(nameof(baseUser.SurName), baseUser.SurName ?? string.Empty),
            new(nameof(baseUser.MobilePhone), baseUser.MobilePhone ?? string.Empty),
            new(nameof(baseUser.UniversityId), baseUser.UniversityId),
            new(nameof(baseUser.AccountStatus), baseUser.AccountStatus.ToString())
        };

        var roleClaims = baseUser.UserRoles?
            .Select(role => new Claim(ClaimTypes.Role, role.ToString()))
            .ToList() ?? new List<Claim>();

        claims.AddRange(roleClaims);

        return new ClaimsIdentity(claims);
    }
}