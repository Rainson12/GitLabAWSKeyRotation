namespace GitLabAWSKeyRotation.Contracts.Gitlab
{
    public record ScanRequest(string name, string token, float rotationIntervalInDays);
}
