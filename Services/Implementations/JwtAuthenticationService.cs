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

    public async Task<AuthToken?> Authenticate(string registrationId, string password, string email)
    {
        var passwordHash = password.Md5Hash();
        var isCredentialsValid = await _baseUserReadRepository.IsCredentialsValidAsync(registrationId, passwordHash);
        if (isCredentialsValid is false) return null;

        // Else we generate JSON Web Token
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!);
        var timeOut = int.Parse(_configuration["JWT:ExpireTimeout"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(timeOut),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new AuthToken { Token = tokenHandler.WriteToken(token) };
    }
}