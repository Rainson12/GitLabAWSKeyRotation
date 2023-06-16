using GitLabAWSKeyRotation.Domain.AWS;
using GitLabAWSKeyRotation.Domain.AWS.Entities;
using GitLabAWSKeyRotation.Domain.AWS.ValueObjects;
using GitLabAWSKeyRotation.Domain.GitLab;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GitLabAWSKeyRotation.Infrastructure.Persistance.Configurations
{
    internal class RotationConfiguration : IEntityTypeConfiguration<Rotation>
    {
        public void Configure(EntityTypeBuilder<Rotation> builder)
        {

            builder.ToTable("RotationDependentRepositoryVariables");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, id => RotationId.Create(id))
                .HasColumnName("RotationDependentRepositoryVariablesId")
                .IsRequired();
            builder.Property(m => m.AccessKeyIdVariableName);
            builder.Property(m => m.AccessSecretVariableName);
            builder.Property(m => m.Environment);

            builder.HasOne<CodeRepository>().WithMany().HasForeignKey(m => m.CodeRepositoryId).IsRequired();

        }
    }
}
