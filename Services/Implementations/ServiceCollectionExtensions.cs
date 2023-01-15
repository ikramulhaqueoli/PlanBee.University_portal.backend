using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace PlanBee.University_portal.backend.Services.Implementations;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUserSignupService, UserSignupService>();
        services.ConfigureAuthentication(configuration);
    }

    private static void ConfigureAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var key = Encoding.UTF8.GetBytes(configuration.GetSection("JWT")["Key"]!);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration.GetSection("JWT")["Issuer"]!,
                ValidAudience = configuration.GetSection("JWT")["Audience"]!,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();
    }
}