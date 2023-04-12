using ErrorOr;

namespace GitLabAWSKeyRotation.Domain.Common.Errors
{
    public static class Rotation
    {
        public static Error RepositoryAlreadyRegistered => Error.Conflict("Repository.AlreadyRegistered", "The GitLab repository is already registered.");
        public static Error DoesNotExist => Error.Conflict("Rotation.RepositoryNotExists", "The GitLab repository does not exist.");

        public static Error AccessKeyIdVariableDoesNotExist => Error.Validation("Rotation.Variables.AccessKeyIdDoesNotExist", "The given variable name for the access key id in the GitLab repository could not be found.");

        public static Error AccessSecretVariableDoesNotExist => Error.Validation("Rotation.Variables.AccessSecretDoesNotExist", "The given variable name for the access secret in the GitLab repository could not be found.");
    }
}
