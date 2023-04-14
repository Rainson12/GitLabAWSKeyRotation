using GitLabAWSKeyRotation.Domain.GitLab;

namespace GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance
{
    public  interface IGitlabAccessTokenRepository
    {
        bool ExistsByName(string name);
        bool Exists(Guid accessTokenId);
        AccessToken? Get(Guid accessTokenId);
        AccessToken? GetByTokenOrName(string token, string name);
        Task<List<AccessToken>> GetAll();
        AccessToken Update(AccessToken accessToken);
        void Add(AccessToken accessToken);
    }
}
