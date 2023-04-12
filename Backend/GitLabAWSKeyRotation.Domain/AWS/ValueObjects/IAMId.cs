using GitLabAWSKeyRotation.Domain.Common.Models;

namespace GitLabAWSKeyRotation.Domain.AWS.ValueObjects
{
    public sealed class IAMId : ValueObject
    {
        public Guid Value { get; }

        private IAMId(Guid value)
        {
            Value = value;
        }

        public static IAMId Create(Guid value)
        {
            return new(value);
        }
        public static IAMId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
