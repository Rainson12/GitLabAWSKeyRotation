namespace GitLabAWSKeyRotation.Contracts.Gitlab
{
    public record RegisterRepositoryRequest(string url, string identifier, string token);
}
