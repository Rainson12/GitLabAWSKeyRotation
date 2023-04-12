using GitLabAWSKeyRotation.Domain.Common.Models;

namespace GitLabAWSKeyRotation.Domain.GitLab.ValueObjects
{
    public sealed class CodeRepositoryId : ValueObject
    {
        public Guid Value { get; }

        private CodeRepositoryId(Guid value)
        {
            Value = value;
        }

        public static CodeRepositoryId Create(Guid value)
        {
            return new(value);
        }
        public static CodeRepositoryId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
