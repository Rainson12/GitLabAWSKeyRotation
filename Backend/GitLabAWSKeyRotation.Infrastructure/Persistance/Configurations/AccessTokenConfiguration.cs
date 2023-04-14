using GitLabAWSKeyRotation.Domain.GitLab;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GitLabAWSKeyRotation.Infrastructure.Persistance.Configurations
{
    internal class AccessTokenConfiguration : IEntityTypeConfiguration<AccessToken>
    {
        public void Configure(EntityTypeBuilder<AccessToken> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                    .ValueGeneratedNever()
                    .HasConversion(id => id.Value, id => AccessTokenId.Create(id))
                    .HasColumnName("AccessTokenId")
                    .IsRequired();
            builder.Property(m => m.TokenName);
            builder.Property(m => m.Token);

           builder.HasMany(m => m.CodeRepositories)
                .WithOne()
                .HasForeignKey("AccessTokenId")
                .IsRequired();
        }
    }
}
