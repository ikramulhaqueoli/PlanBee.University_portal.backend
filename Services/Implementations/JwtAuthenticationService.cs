using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlanBee.University_portal.backend.Domain.Entities.BaseUserDomain;
using PlanBee.University_portal.backend.Domain.Libraries;
using PlanBee.University_portal.backend.Services.Models;

namespace PlanBee.University_portal.backend.Services.Implementations;

public class JwtAuthenticationService : IJwtAuthenticationService
{
    private readonly IBaseUserReadRepository _baseUserReadRepository;
    private readonly IConfiguration _configuration;

    public JwtAuthenticationService(
        IConfiguration configuration,
        IBaseUserReadRepository baseUserReadRepository)
    {
        _configuration = configuration;
        _baseUserReadRepository = baseUserReadRepository;
    }

    public async Task<AuthToken?> Authenticate(string registrationId, string password)
    {
        var passwordHash = password.Md5Hash();
        var baseUser = await _baseUserReadRepository.GetByCredentialsAsync(registrationId, passwordHash);
        if (baseUser == null) return null;

        // Else we generate JSON Web Token
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!);
        var timeOut = int.Parse(_configuration["JWT:ExpireTimeout"]!);
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
        return new(new[]
        {
            new Claim(nameof(baseUser.ItemId), baseUser.ItemId.ToString()),
            new Claim(nameof(baseUser.Email), baseUser.Email ?? string.Empty),
            new Claim(nameof(baseUser.UniversityEmail), baseUser.UniversityEmail ?? string.Empty),
            new Claim(nameof(baseUser.DateOfBirth), baseUser.DateOfBirth?.ToString() ?? string.Empty),
            new Claim(nameof(baseUser.FirstName), baseUser.FirstName),
            new Claim(nameof(baseUser.LastName), baseUser.LastName),
            new Claim(nameof(baseUser.Gender), baseUser.Gender.ToString()),
            new Claim(nameof(baseUser.SurName), baseUser.SurName ?? string.Empty),
            new Claim(nameof(baseUser.MobilePhone), baseUser.MobilePhone ?? string.Empty),
            new Claim(nameof(baseUser.RegistrationId), baseUser.RegistrationId),
            new Claim(nameof(baseUser.UserRole), baseUser.UserRole.ToString()),
            new Claim(nameof(baseUser.Activate), baseUser.IsActive.ToString())
        });
    }
}