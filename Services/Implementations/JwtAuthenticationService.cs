using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

    public async Task<AuthToken?> Authenticate(string registrationId, string password)
    {
        var passwordHash = password.Md5Hash();
        var baseUser = await _baseUserReadRepository.GetByCredentialsAsync(registrationId, passwordHash);
        if (baseUser == null) return null;

        // Else we generate JSON Web Token
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenKey = Encoding.UTF8.GetBytes(AppConfigUtil.Config.Jwt.Key!);
        var timeOut = AppConfigUtil.Config.Jwt.ExpireTimeout;
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GetClaimsIdentity(baseUser),
            Expires = DateTime.UtcNow.AddMinutes(timeOut),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new AuthToken { Token = tokenHandler.WriteToken(token) };
    }

    private ClaimsIdentity GetClaimsIdentity(BaseUser baseUser)
    {
        var claims = new List<Claim>
        {
            new(nameof(baseUser.ItemId), baseUser.ItemId.ToString()),
            new(nameof(baseUser.Email), baseUser.Email ?? string.Empty),
            new(nameof(baseUser.UniversityEmail), baseUser.UniversityEmail ?? string.Empty),
            new(nameof(baseUser.DateOfBirth), baseUser.DateOfBirth.ToString() ?? string.Empty),
            new(nameof(baseUser.FirstName), baseUser.FirstName),
            new(nameof(baseUser.LastName), baseUser.LastName),
            new(nameof(baseUser.Gender), baseUser.Gender.ToString()),
            new(nameof(baseUser.SurName), baseUser.SurName ?? string.Empty),
            new(nameof(baseUser.MobilePhone), baseUser.MobilePhone ?? string.Empty),
            new(nameof(baseUser.RegistrationId), baseUser.RegistrationId),
            new(nameof(baseUser.Activate), baseUser.IsActive.ToString())
        };

        var roleClaims = baseUser.UserRoles?
                             .Select(role => new Claim(ClaimTypes.Role, role.ToString())).ToList()
                         ?? new List<Claim>();

        claims.AddRange(roleClaims);

        return new ClaimsIdentity(claims);
    }
}