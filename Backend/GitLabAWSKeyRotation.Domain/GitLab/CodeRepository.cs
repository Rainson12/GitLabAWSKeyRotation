using GitLabAWSKeyRotation.Domain.Common.Models;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;

namespace GitLabAWSKeyRotation.Domain.GitLab
{
    public sealed class CodeRepository : Entity<CodeRepositoryId>
    {
        public CodeRepository(CodeRepositoryId id, string url, string name) : base(id)
        {
            Url = url;
            Name = name;
        }

        public string Url { get; private set; }
        public string Name { get; private set; }

        public static CodeRepository Create(string url, string name)
        {
            return new(CodeRepositoryId.CreateUnique(), url, name);
        }

        #pragma warning disable CS8618
        private CodeRepository()
        {

        }
        #pragma warning restore CS8618
    }
}
