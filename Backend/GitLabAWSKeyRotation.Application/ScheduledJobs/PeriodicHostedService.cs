using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GitLabAWSKeyRotation.Application.ScheduledJobs
{


    internal abstract class PeriodicHostedService<T> : BackgroundService where T : IPeriodicTask
    {
        private readonly TimeSpan _period;
        private readonly IServiceScopeFactory _factory;
        private int _executionCount = 0;
        private bool _runOnStartup = false;

        public PeriodicHostedService(IServiceScopeFactory factory, int intervalInSeconds, bool runOnStartup)
        {
            _factory = factory;
            _period = TimeSpan.FromSeconds(intervalInSeconds);
            _runOnStartup = runOnStartup;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (
                !stoppingToken.IsCancellationRequested &&
                _runOnStartup || await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    _runOnStartup = false;
                    await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                    var service = asyncScope.ServiceProvider.GetRequiredService<T>();
                    await service.DoWork();
                    _executionCount++;
                }
                catch (Exception ex)
                {
                    // TODO: Log exception
                }
            }
        }
    }
}
