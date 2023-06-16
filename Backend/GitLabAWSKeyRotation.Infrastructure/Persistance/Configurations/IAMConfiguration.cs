using GitLabAWSKeyRotation.Domain.AWS;
using GitLabAWSKeyRotation.Domain.AWS.Entities;
using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;
using GitLabAWSKeyRotation.Domain.GitLab;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GitLabAWSKeyRotation.Infrastructure.Persistance.Configurations
{
    internal class IAMConfiguration : IEntityTypeConfiguration<IAM>
    {
        public void Configure(EntityTypeBuilder<IAM> builder)
        {
            builder.ToTable("AWSIAMs");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, id => IAMId.Create(id))
                .HasColumnName("IamId")
                .IsRequired();

            builder.Property(m => m.Name);
            builder.Property(m => m.Arn);
            builder.Property(m => m.KeyRotationInDays);
            builder.Property(m => m.AccessKeyId);
            builder.Property(m => m.AccessSecret);

        }
    }
}
