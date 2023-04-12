namespace GitLabAWSKeyRotation.Application.ScheduledJobs
{
    internal interface IPeriodicTask
    {
        public Task DoWork();
    }
}