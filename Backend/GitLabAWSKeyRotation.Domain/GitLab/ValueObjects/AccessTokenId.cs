using GitLabAWSKeyRotation.Domain.Common.Models;

namespace GitLabAWSKeyRotation.Domain.GitLab.ValueObjects
{
    public sealed class AccessTokenId : ValueObject
    {
        public Guid Value { get; }

        private AccessTokenId(Guid value)
        {
            Value = value;
        }

        public static AccessTokenId Create(Guid value)
        {
            return new(value);
        }
        public static AccessTokenId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
