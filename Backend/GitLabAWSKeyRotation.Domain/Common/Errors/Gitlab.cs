using ErrorOr;

namespace GitLabAWSKeyRotation.Domain.Common.Errors
{
    public static class Gitlab
    {
        public static Error AccessTokenAlreadyRegistered => Error.Conflict("Gitlab.AccessTokenAlreadyRegistered", "The access token is already registered.");
        public static Error RepositoryAlreadyRegistered => Error.Conflict("Gitlab.RepositoryAlreadyRegistered", "The repository is already registered.");
        public static Error AccessTokenDoesNotExist => Error.Validation("Gitlab.AccessTokenDoesNotExist", "The access token does not exist.");
        public static Error RepositoryDoesNotExist => Error.Validation("Gitlab.RepositoryDoesNotExist", "The repository does not exist.");
    }
}
