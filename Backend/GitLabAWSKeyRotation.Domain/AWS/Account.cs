using GitLabAWSKeyRotation.Domain.AWS.Entities;
using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;
using GitLabAWSKeyRotation.Domain.Common.Models;

namespace GitLabAWSKeyRotation.Domain.AWS
{
    public sealed class Account : AggregateRoot<AccountId>
    {
        private Account(AccountId id, string awsAccountId, string displayName) : base(id)
        {
            AwsAccountId = awsAccountId;
            DisplayName = displayName;
        }
        private readonly List<IAM> _iamIdentities = new();

        public string AwsAccountId { get; private set; }
        public string DisplayName { get; private set; }
        public IReadOnlyList<IAM> IamIdentities => _iamIdentities.AsReadOnly();

        public void AddIdentity(IAM identity)
        {
            _iamIdentities.Add(identity);
        }

        public static Account Create(string awsAccountId, string displayName)
        {
            return new (AccountId.CreateUnique(), awsAccountId, displayName);
        }

#pragma warning disable CS8618
        public Account()
        {

        }
#pragma warning restore CS8618
    }
}
