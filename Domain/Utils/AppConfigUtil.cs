using Newtonsoft.Json;
using PlanBee.University_portal.backend.Domain.Models;

namespace PlanBee.University_portal.backend.Domain.Utils
{
    public static class AppConfigUtil
    {
        private const string CONFIG_NAME = "appconfig";
        private const string ENV_VARIABLE_NAME = "ASPNETCORE_ENVIRONMENT";

        private static AppConfig? _config = null;

        public static AppConfig Config 
        {
            get 
            {
                _config ??= GetConfig();
                return _config;
            }
        }

        private static AppConfig GetConfig()
        {
            var commonConfigPath = $"{Directory.GetCurrentDirectory()}//{CONFIG_NAME}.json";
            
            var environmentName = Environment.GetEnvironmentVariable(ENV_VARIABLE_NAME);
            var environmentConfigPath = $"{Directory.GetCurrentDirectory()}//{CONFIG_NAME}.{environmentName}.json";
            
            var commonConfigJson = File.ReadAllText(commonConfigPath);
            var environmentConfigJson = File.ReadAllText(environmentConfigPath);

            var appConfig = JsonConvert.DeserializeObject<AppConfig>(commonConfigJson);
            var environmentConfig = JsonConvert.DeserializeObject<AppConfig>(environmentConfigJson);

            CopyNonNullNonEmptyValues(environmentConfig!, appConfig!);
            return appConfig!;
        }

        private static void CopyNonNullNonEmptyValues(object source, object destination)
        {
            var sourceProperties = source.GetType().GetProperties();
            var destinationProperties = destination.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name);

                if (destinationProperty != null)
                {
                    var sourceValue = sourceProperty.GetValue(source);
                    var destinationValue = destinationProperty.GetValue(destination);

                    // Check if the source property has a non-null and non-empty value.
                    if (sourceValue != null && !string.IsNullOrEmpty(sourceValue.ToString()))
                    {
                        // Copy the value to the destination object.
                        destinationProperty.SetValue(destination, sourceValue);
                    }
                }
            }
        }
    }
}
