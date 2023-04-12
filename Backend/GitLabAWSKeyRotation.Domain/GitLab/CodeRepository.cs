using GitLabAWSKeyRotation.Domain.Common.Models;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;

namespace GitLabAWSKeyRotation.Domain.GitLab
{
    public sealed class CodeRepository : AggregateRoot<CodeRepositoryId>
    {
        public CodeRepository(CodeRepositoryId id, string url, AccessKey accessKey) : base(id)
        {
            Url = url;
            AccessKey = accessKey;
        }

        public string Url { get; private set; }
        public AccessKey AccessKey { get; private set; }

        public static CodeRepository Create(string url, AccessKey accessKey)
        {
            return new(CodeRepositoryId.CreateUnique(), url, accessKey);
        }

        #pragma warning disable CS8618
        private CodeRepository()
        {

        }
        #pragma warning restore CS8618
    }
}
