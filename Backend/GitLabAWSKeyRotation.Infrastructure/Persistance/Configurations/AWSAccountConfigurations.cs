using GitLabAWSKeyRotation.Domain.AWS;
using GitLabAWSKeyRotation.Domain.AWS.Entities;
using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;
using GitLabAWSKeyRotation.Domain.GitLab;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GitLabAWSKeyRotation.Infrastructure.Persistance.Configurations
{
    internal class AWSAccountConfigurations : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("AWSAccounts");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, id => AccountId.Create(id))
                .HasColumnName("AccountId")
                .IsRequired();

            builder.Property(m => m.AwsAccountId)
                .IsRequired();

            builder.Property(m => m.DisplayName)
                .IsRequired();

        }

    }
}
