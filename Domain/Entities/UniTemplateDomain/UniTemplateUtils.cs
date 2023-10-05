namespace PlanBee.University_portal.backend.Domain.Entities.UniTemplateDomain
{
    public static class UniTemplateExtensions
    {
        public static void ResolveTemplate(
            this UniTemplate template,
            Dictionary<string, string> placeHolderValuePairs)
        {
            foreach (var placeHolderKV in placeHolderValuePairs)
            {
                template.Body.Replace($"{{{placeHolderKV.Key}}}", placeHolderKV.Value);
                template.Subject?.Replace($"{{{placeHolderKV.Key}}}", placeHolderKV.Value);
            }
        }
    }
}
