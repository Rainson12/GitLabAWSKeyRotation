namespace GitLabAWSKeyRotation.Contracts.Gitlab
{
    public record RegisterRotationRequest(string environment, string accessKeyIdVariableName, string accessSecretVariableName, Guid CodeRepositoryId, Guid IamId, Guid AwsAccountId);
}
