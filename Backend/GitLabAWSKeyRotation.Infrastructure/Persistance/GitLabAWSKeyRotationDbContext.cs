using GitLabAWSKeyRotation.Domain.AWS;
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
        public DbSet<CodeRepository> CodeRepositories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GitLabAWSKeyRotationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
