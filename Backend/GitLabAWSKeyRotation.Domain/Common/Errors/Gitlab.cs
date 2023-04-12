using ErrorOr;

namespace GitLabAWSKeyRotation.Domain.Common.Errors
{
    public static class Gitlab
    {
        public static Error AlreadyRegistered => Error.Conflict("Gitlab.RepositoryAlreadyRegistered", "The repository is already registered.");
        public static Error DoesNotExist => Error.Validation("Gitlab.RepositoryDoesNotExist", "The repository does not exist.");
    }
}
