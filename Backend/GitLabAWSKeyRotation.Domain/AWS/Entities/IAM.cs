using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;
using GitLabAWSKeyRotation.Domain.Common.Models;
using GitLabAWSKeyRotation.Domain.GitLab;

namespace GitLabAWSKeyRotation.Domain.AWS.Entities
{
    public sealed class IAM : Entity<IAMId>
    {
        private IAM(IAMId id, string arn, string name, string accessKeyId, string accessSecret, float rotationInDays) : base(id)
        {
            Arn = arn;
            Name = name;
            AccessKeyId = accessKeyId;
            AccessSecret = accessSecret;
            KeyRotationInDays = rotationInDays;
        }
        private readonly List<Rotation> _rotations = new();

        public string Arn { get; private set; }
        public string Name { get; private set; }
        public string AccessKeyId { get; private set; }
        public string AccessSecret { get; private set; }

        public IReadOnlyList<Rotation> Rotations => _rotations.AsReadOnly();
        public float KeyRotationInDays { get; private set; }

        public static IAM Create(string arn, string name, string accessKeyId, string accessSecret, float rotationInDays)
        {
            return new (IAMId.CreateUnique(), arn, name, accessKeyId, accessSecret, rotationInDays);
        }

        public void UpdateKey(string accessKeyId, string accessSecret)
        {
            AccessKeyId = accessKeyId;
            AccessSecret = accessSecret;
        }

        public void AddRotation(Rotation rotation)
        {
            _rotations.Add(rotation);
        }

#pragma warning disable CS8618
        public IAM()
        {

        }
#pragma warning restore CS8618
    }
}
