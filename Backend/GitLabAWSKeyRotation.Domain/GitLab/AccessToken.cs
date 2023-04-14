using GitLabAWSKeyRotation.Domain.Common.Models;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;

namespace GitLabAWSKeyRotation.Domain.GitLab
{
    public sealed class AccessToken : AggregateRoot<AccessTokenId>
    {
        public AccessToken(AccessTokenId id, string tokenName, string token) : base(id)
        {

            TokenName = tokenName;
            Token = token;
        }
        private readonly List<CodeRepository> _codeRepositories = new();

        public string TokenName { get; set; }
        public string Token { get; private set; }
        public IReadOnlyList<CodeRepository> CodeRepositories=> _codeRepositories.AsReadOnly();

        public static AccessToken Create(string tokenName, string token)
        {
            return new(AccessTokenId.CreateUnique(), tokenName, token);
        }
        public void AddCodeRepository(CodeRepository repository)
        {
            _codeRepositories.Add(repository);
        }

#pragma warning disable CS8618
        private AccessToken()
        {

        }
        #pragma warning restore CS8618
    }
}
