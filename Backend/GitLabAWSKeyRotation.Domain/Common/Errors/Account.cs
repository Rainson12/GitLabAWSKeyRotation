using ErrorOr;

namespace GitLabAWSKeyRotation.Domain.Common.Errors
{
    public static class Account
    {
        public static Error AlreadyRegistered => Error.Conflict("Account.AlreadyRegistered", "The AWS Account is already registered.");
        public static Error DoesNotExist => Error.Validation("Account.NotExists", "The AWS Account does not exist.");
    }
}
