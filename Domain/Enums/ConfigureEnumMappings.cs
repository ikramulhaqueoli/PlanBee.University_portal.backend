using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PlanBee.University_portal.backend.Domain.Enums.Business;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PlanBee.University_portal.backend.Domain.Enums
{
    public static class ConfigureSwaggerEnumMappings
    {
        public static void ConfigureEnumMappings(this SwaggerGenOptions options)
        {
            options.MapType<UserRole>(() => new OpenApiSchema { Type = "string" });
            options.MapType<RegistrationActionStatus>(() => new OpenApiSchema { Type = "string" });
            options.MapType<AccountStatus>(() => new OpenApiSchema { Type = "string" });
            options.MapType<DesignationType>(() => new OpenApiSchema { Type = "string" });
            options.MapType<Gender>(() => new OpenApiSchema { Type = "string" });
            options.MapType<UserType>(() => new OpenApiSchema { Type = "string" });
            options.MapType<UserVerificationMedia>(() => new OpenApiSchema { Type = "string" });
            options.MapType<UserVerificationType>(() => new OpenApiSchema { Type = "string" });
            options.MapType<WorkplaceType>(() => new OpenApiSchema { Type = "string" });
        }
    }
}
