using ErrorOr;

namespace GitLabAWSKeyRotation.Domain.Common.Errors
{
    public static class IAM
    {
        public static Error AlreadyRegistered => Error.Conflict("IAM.AlreadyRegistered", "The IAM is already registered.");
        public static Error DoesNotExist => Error.Validation("IAM.NotExists", "The IAM Account does not exist.");
        public static Error InvalidAccessCredentials => Error.Validation("IAM.InvalidAccessCredentials", "The provided access key or secret is invalid.");
    }
}
