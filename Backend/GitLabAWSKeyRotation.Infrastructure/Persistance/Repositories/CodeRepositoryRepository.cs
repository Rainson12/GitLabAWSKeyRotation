using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Domain.GitLab;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GitLabAWSKeyRotation.Infrastructure.Persistance.Repositories
{
    public class CodeRepositoryRepository : ICodeRepositoryRepository
    {
        private readonly GitLabAWSKeyRotationDbContext _dbContext;

        public CodeRepositoryRepository(GitLabAWSKeyRotationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(CodeRepository repository)
        {
            _dbContext.Add(repository);
            _dbContext.SaveChanges();
        }

        public bool Exists(Guid repositoryId)
        {
            return _dbContext.CodeRepositories.Any(x => x.Id.Value == repositoryId);
        }

        public bool ExistsByUrl(string url)
        {
            return _dbContext.CodeRepositories.Any(x => x.Url == url);
        }

        public CodeRepository? Get(Guid repositoryId)
        {
            return _dbContext.CodeRepositories.FirstOrDefault(x => x.Id == CodeRepositoryId.Create(repositoryId));
        }

        public Task<List<CodeRepository>> GetAll()
        {
            return _dbContext.CodeRepositories.ToListAsync();
        }
        public CodeRepository Update(CodeRepository repository)
        {
            _dbContext.Update(repository);
            _dbContext.SaveChanges();
            return repository;
        }
    }
}
