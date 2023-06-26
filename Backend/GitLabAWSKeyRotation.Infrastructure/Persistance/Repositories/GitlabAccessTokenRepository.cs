using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Domain.GitLab;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GitLabAWSKeyRotation.Infrastructure.Persistance.Repositories
{
    public class GitlabAccessTokenRepository : IGitlabAccessTokenRepository
    {
        private readonly GitLabAWSKeyRotationDbContext _dbContext;

        public GitlabAccessTokenRepository(GitLabAWSKeyRotationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(AccessToken accessToken)
        {
            _dbContext.Add(accessToken);
            _dbContext.SaveChanges();
        }

        public bool Exists(Guid accessTokenId)
        {
            return _dbContext.GitlabAccessTokens.Any(x => x.Id.Value == accessTokenId);
        }
        public bool ExistsByName(string name)
        {
            return _dbContext.GitlabAccessTokens.Any(x => x.TokenName == name);
        }


        public AccessToken? Get(Guid accessTokenId)
        {
            return _dbContext.GitlabAccessTokens.FirstOrDefault(x => x.Id == AccessTokenId.Create(accessTokenId));
        }
        public AccessToken? GetByTokenOrName(string token, string name)
        {
            return _dbContext.GitlabAccessTokens.Include(x => x.CodeRepositories).FirstOrDefault(x => x.Token == token || x.TokenName == name);
        }

        public Task<List<AccessToken>> GetAll()
        {
            return _dbContext.GitlabAccessTokens.ToListAsync();
        }

        public async Task<List<CodeRepository>> GetCodeRepositories(AccessTokenId accessTokenId)
        {
            var result = await _dbContext.GitlabAccessTokens.Include(x => x.CodeRepositories).FirstOrDefaultAsync(x => x.Id == accessTokenId);
            return result?.CodeRepositories.ToList() ?? new List<CodeRepository>();
        }

        public Task<List<Rotation>> GetRotations(CodeRepositoryId codeRepositoryId)
        {
            return _dbContext.Rotations.Where(x => x.CodeRepositoryId == codeRepositoryId).ToListAsync();
        }

        public AccessToken Update(AccessToken accessToken)
        {
            _dbContext.Update(accessToken);
            _dbContext.SaveChanges();
            return accessToken;
        }

        public Task<List<AccessToken>> GetAllWithAllSubProperties()
        {
            return _dbContext.GitlabAccessTokens.Include(x => x.CodeRepositories).ToListAsync();
        }
    }
}
