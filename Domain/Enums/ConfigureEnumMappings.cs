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
            var schema = new OpenApiSchema { Type = "string" };
            options.MapType<UserRole>(() => schema);
            options.MapType<RegistrationActionStatus>(() => schema);
            options.MapType<AccountStatus>(() => schema);
            options.MapType<DesignationType>(() => schema);
            options.MapType<Gender>(() => schema);
            options.MapType<UserType>(() => schema);
            options.MapType<UserVerificationMedia>(() => schema);
            options.MapType<UserVerificationType>(() => schema);
            options.MapType<WorkplaceType>(() => schema);
        }
    }
}
