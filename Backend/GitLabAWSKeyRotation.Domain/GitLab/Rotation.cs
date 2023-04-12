using GitLabAWSKeyRotation.Domain.Common.Models;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;

namespace GitLabAWSKeyRotation.Domain.GitLab
{
    public class Rotation : Entity<RotationId>
    {
        public Rotation(RotationId id, string environment, string accessKeyIdVariableName, string accessSecretVariableName, CodeRepositoryId codeRepositoryId) : base(id)
        {
            Environment = environment;
            AccessKeyIdVariableName = accessKeyIdVariableName;
            AccessSecretVariableName = accessSecretVariableName;
            CodeRepositoryId = codeRepositoryId;
        }

        public string Environment { get; private set; }
        public string AccessKeyIdVariableName { get; private set; }
        public string AccessSecretVariableName { get; private set; }
        public CodeRepositoryId CodeRepositoryId { get; private set; }

        public static Rotation Create(string environment, string accessKeyIdVariableName, string accessSecretVariableName, CodeRepositoryId codeRepositoryId)
        {
            return new(RotationId.CreateUnique(), environment, accessKeyIdVariableName, accessSecretVariableName, codeRepositoryId);
        }

#pragma warning disable CS8618
        public Rotation()
        {

        }
#pragma warning restore CS8618
    }
}
