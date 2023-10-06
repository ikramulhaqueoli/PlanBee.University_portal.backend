namespace PlanBee.University_portal.backend.Domain.Entities.UniTemplateDomain
{
    public static class UniTemplateExtensions
    {
        public static void ResolveTemplate(
            this UniTemplate template,
            Dictionary<string, string> placeHolderValuePairs)
        {
            var resolvedBody = template.Body;
            var resolvedSubject = template.Subject;

            foreach (var placeHolderKV in placeHolderValuePairs)
            {
                resolvedBody = resolvedBody.Replace("{{" + placeHolderKV.Key + "}}", placeHolderKV.Value);
                resolvedSubject = resolvedSubject?.Replace("{{" + placeHolderKV.Key + "}}", placeHolderKV.Value);
            }

            template.Subject = resolvedSubject;
            template.Body = resolvedBody;
        }
    }
}
