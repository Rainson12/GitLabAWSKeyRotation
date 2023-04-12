using GitLabAWSKeyRotation.Domain.Common.Models;

namespace GitLabAWSKeyRotation.Domain.GitLab.ValueObjects
{
    public sealed class RotationId : ValueObject
    {
        public Guid Value { get; }

        private RotationId(Guid value)
        {
            Value = value;
        }

        public static RotationId Create(Guid value)
        {
            return new(value);
        }
        public static RotationId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
