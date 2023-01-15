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
        => new (claims: new[]
        {
            new Claim(type: nameof(baseUser.ItemId), value: baseUser.ItemId.ToString()),
            new Claim(type: nameof(baseUser.Email), value: baseUser.Email!),
            new Claim(type: nameof(baseUser.UniversityEmail), value: baseUser.UniversityEmail!),
            new Claim(type: nameof(baseUser.DateOfBirth), value: baseUser.DateOfBirth.ToString()!),
            new Claim(type: nameof(baseUser.FirstName), value: baseUser.FirstName),
            new Claim(type: nameof(baseUser.LastName), value: baseUser.LastName),
            new Claim(type: nameof(baseUser.Gender), value: baseUser.Gender.ToString()),
            new Claim(type: nameof(baseUser.SurName), value: baseUser.SurName!),
            new Claim(type: nameof(baseUser.MobilePhone), value: baseUser.MobilePhone!),
            new Claim(type: nameof(baseUser.RegistrationId), value: baseUser.RegistrationId),
            new Claim(type: nameof(baseUser.UserRole), value: baseUser.UserRole.ToString()),
            new Claim(type: nameof(baseUser.Activate), value: baseUser.IsActive.ToString()),
        });
}