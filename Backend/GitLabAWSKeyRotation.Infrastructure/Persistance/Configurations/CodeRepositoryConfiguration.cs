using GitLabAWSKeyRotation.Domain.GitLab;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GitLabAWSKeyRotation.Infrastructure.Persistance.Configurations
{
    internal class CodeRepositoryConfiguration : IEntityTypeConfiguration<CodeRepository>
    {
        public void Configure(EntityTypeBuilder<CodeRepository> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                    .ValueGeneratedNever()
                    .HasConversion(id => id.Value, id => CodeRepositoryId.Create(id))
                    .HasColumnName("CodeRepositoryId")
                    .IsRequired();

            builder.Property(m => m.Url);
            builder.OwnsOne(m => m.AccessKey);
        }
    }
}
