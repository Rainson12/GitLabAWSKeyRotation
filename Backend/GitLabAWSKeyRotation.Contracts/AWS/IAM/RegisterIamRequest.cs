namespace GitLabAWSKeyRotation.Contracts.AWS.IAM
{
    public record RegisterIamRequest(string accountId, string name, string accessKeyId, string accessSecret, float rotationInDays);
}
