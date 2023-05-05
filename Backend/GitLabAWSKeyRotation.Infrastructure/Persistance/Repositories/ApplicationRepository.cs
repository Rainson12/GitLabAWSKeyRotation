using Amazon.SecurityToken.Model;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Domain.AWS;
using GitLabAWSKeyRotation.Domain.AWS.Entities;
using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;
using GitLabAWSKeyRotation.Domain.Common.Errors;
using Microsoft.EntityFrameworkCore;

namespace GitLabAWSKeyRotation.Infrastructure.Persistance.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly GitLabAWSKeyRotationDbContext _dbContext = null!;

        public ApplicationRepository(GitLabAWSKeyRotationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Application.Application> Create(Domain.Application.Application app)
        {
            if(await IsConfigured())
                throw new Exception("App already configured");

            _dbContext.Add(app);
            _dbContext.SaveChanges();
            return app;
        }

        public async Task<Domain.Application.Application> Update(Domain.Application.Application app)
        {
            _dbContext.Update(app);
            _dbContext.SaveChanges();
            return app;
        }

        public async Task<bool> IsConfigured()
        {
            return await _dbContext.Applications.AnyAsync();
        }

        public async Task<Domain.Application.Application> Get()
        {
            return await _dbContext.Applications.FirstOrDefaultAsync();
        }
    }
}
