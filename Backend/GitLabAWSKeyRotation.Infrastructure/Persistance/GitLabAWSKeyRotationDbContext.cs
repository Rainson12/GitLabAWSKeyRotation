using GitLabAWSKeyRotation.Domain.AWS;
using GitLabAWSKeyRotation.Domain.AWS.Entities;
using GitLabAWSKeyRotation.Domain.GitLab;
using Microsoft.EntityFrameworkCore;

namespace GitLabAWSKeyRotation.Infrastructure.Persistance
{
    public class GitLabAWSKeyRotationDbContext : DbContext
    {
        public GitLabAWSKeyRotationDbContext(DbContextOptions<GitLabAWSKeyRotationDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<AccessToken> GitlabAccessTokens { get; set; } = null!;
        public DbSet<Domain.Application.Application> Applications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GitLabAWSKeyRotationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
