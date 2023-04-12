using GitLabAWSKeyRotation.Domain.Common.Models;

namespace GitLabAWSKeyRotation.Domain.GitLab.ValueObjects
{
    public sealed class AccessKey : ValueObject
    {
        public string Identifier { get; private set; }
        public string Token { get; private set; }

        private AccessKey (string identifier, string token)
        {
            Identifier = identifier;
            Token = token;
        }

        public static AccessKey Create(string identifier, string token)
        {
            return new(identifier, token);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Identifier;
            yield return Token;
        }

#pragma warning disable CS8618
        private AccessKey()
        {

        }
#pragma warning restore CS8618
    }
}
