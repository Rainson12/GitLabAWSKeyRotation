namespace GitLabAWSKeyRotation.Contracts.Gitlab
{
    public record RegisterRotationRequest(string environment, string accessKeyIdVariableName, string accessSecretVariableName, Guid GitlabAccessTokenId, Guid CodeRepositoryId, Guid IamId, Guid AwsAccountId);
}
