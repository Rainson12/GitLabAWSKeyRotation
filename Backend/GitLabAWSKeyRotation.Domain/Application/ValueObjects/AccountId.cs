using GitLabAWSKeyRotation.Domain.Common.Models;

namespace GitLabAWSKeyRotation.Domain.Application.ValueObjects
{
    public sealed class ApplicationId : ValueObject
    {
        public Guid Value { get; }

        private ApplicationId(Guid value)
        {
            Value = value;
        }

        public static ApplicationId Create(Guid value)
        {
            return new(value);
        }
        public static ApplicationId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
