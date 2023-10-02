using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PlanBee.University_portal.backend.Domain.Models;

namespace PlanBee.University_portal.backend.Services.Implementations;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services, AppConfig appConfig)
    {
        services.AddTransient<ISeedDataService, SeedDataService>();
        services.AddTransient<IEmployeeSignupService, EmployeeSignupService>();
        services.ConfigureAuthentication(appConfig);
    }

    private static void ConfigureAuthentication(
        this IServiceCollection services,
        AppConfig appConfig)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var key = Encoding.UTF8.GetBytes(appConfig.Jwt.Key!);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = appConfig.Jwt.Issuer!,
                ValidAudience = appConfig.Jwt.Audience!,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();
    }
}