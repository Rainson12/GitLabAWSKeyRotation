using GitLabAWSKeyRotation.Domain.GitLab;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;

namespace GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance
{
    public  interface IGitlabAccessTokenRepository
    {
        bool ExistsByName(string name);
        bool Exists(Guid accessTokenId);
        AccessToken? Get(Guid accessTokenId);
        AccessToken? GetByTokenOrName(string token, string name);
        Task<List<AccessToken>> GetAll();
        Task<List<CodeRepository>> GetCodeRepositories(AccessTokenId accessTokenId);
        Task<List<Rotation>> GetRotations(CodeRepositoryId codeRepositoryId);
        AccessToken Update(AccessToken accessToken);
        void Add(AccessToken accessToken);
    }
}
