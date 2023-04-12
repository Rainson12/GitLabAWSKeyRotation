using GitLabAWSKeyRotation.Application.ScheduledJobs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GitLabAWSKeyRotation.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddPeriodicServiceExecutions();
            return services;
        }

        public static IServiceCollection AddPeriodicServiceExecutions(this IServiceCollection services)
        {
            services.AddScoped<PeriodicKeyRotationService>();
            services.AddHostedService<PeriodicKeyRotation>();
            return services;
        }
    }
}
