using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PlanBee.University_portal.backend.Domain.Models;
using System.Text;

namespace PlanBee.University_portal.backend.Services.Implementations;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services, AppConfig appConfig)
    {
        services.AddTransient<IUserSignupService, UserSignupService>();

        services.AddTransient<ISpecificSignupService, EmployeeSignupService>();
        services.AddTransient<ISpecificSignupService, StudentSignupService>();

        services.AddTransient<IUniversityEmailService, UniversityEmailService>();
        services.AddTransient<IUserVerificationService, UserVerificationService>();

        services.AddTransient<IUniAdministrativeService, UniAdministrativeService>();

        services.AddTransient<IJwtAuthenticationService, JwtAuthenticationService>();
        services.ConfigureAuthentication(appConfig);

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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