using Infrastructure;
using Infrastructure.Implementations;
using Microsoft.Extensions.DependencyInjection;
using PlanBee.University_portal.backend.Infrastructure.Implementations;

namespace PlanBee.University_portal.backend.Infrastructure
{
    public static class ServiceColelctionExtensions
    {
        public static void AddInsfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ISeedDataManager, SeedDataManager>();
            services.AddTransient<IEmailSender, GmailEmailSender>();
            services.AddTransient<IGraphQlService, GraphQlService>();
        }
    }
}
