using Microsoft.Extensions.DependencyInjection;

namespace GitLabAWSKeyRotation.Application.ScheduledJobs
{
    internal class PeriodicKeyRotation : PeriodicHostedService<PeriodicKeyRotationService>
    {
        public PeriodicKeyRotation(IServiceScopeFactory factory, int intervalInSeconds = 1 * 60 * 60 * 24, bool runOnStartup = true) : base(factory, intervalInSeconds, runOnStartup)
        {
        }
    }
}
