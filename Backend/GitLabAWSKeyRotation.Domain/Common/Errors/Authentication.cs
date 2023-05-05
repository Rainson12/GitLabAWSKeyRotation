using ErrorOr;

namespace GitLabAWSKeyRotation.Domain.Common.Errors
{
    public static class Authentication
    {
        public static Error NotConfigured => Error.Conflict("Authentication.NotConfigured", "The authentication is not configured.");
        public static Error AlreadyConfigured => Error.Conflict("Authentication.AlreadyConfigured", "The authentication is already configured.");
        public static Error IncorrectAuthKey => Error.Validation("Authentication.IncorrectAuthKey", "The authentication failed.");
    }
}
