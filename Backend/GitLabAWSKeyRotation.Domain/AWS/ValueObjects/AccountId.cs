using GitLabAWSKeyRotation.Domain.Common.Models;

namespace GitLabAWSKeyRotation.Domain.AWS.ValueObjects
{
    public sealed class AccountId : ValueObject
    {
        public Guid Value { get; }

        private AccountId(Guid value)
        {
            Value = value;
        }

        public static AccountId Create(Guid value)
        {
            return new(value);
        }
        public static AccountId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
