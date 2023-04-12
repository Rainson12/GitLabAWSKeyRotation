using GitLabAWSKeyRotation.Domain.AWS;
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
            ConfigureAWSAccountTable(builder);
            ConfigureIAMTable(builder);

            //builder.Metadata.FindNavigation(nameof(Account.IamIdentities))!.SetPropertyAccessMode(PropertyAccessMode.Field);
            
        }

        private void ConfigureIAMTable(EntityTypeBuilder<Account> builder)
        {
            builder.OwnsMany(m => m.IamIdentities, ib =>
            {
                ib.ToTable("AWSIAMs");
                ib.WithOwner().HasForeignKey("AWSAccountId");

                ib.HasKey(m => m.Id);
                ib.Property(m => m.Id)
                    .ValueGeneratedNever()
                    .HasConversion(id => id.Value, id => IAMId.Create(id))
                    .HasColumnName("IamId")
                    .IsRequired();

                ib.Property(m => m.Name);
                ib.Property(m => m.Arn);
                ib.Property(m => m.KeyRotationInDays);
                ib.Property(m => m.AccessKeyId);
                ib.Property(m => m.AccessSecret);

                ib.OwnsMany(i => i.Rotations, db =>
                {
                    db.ToTable("RotationDependentRepositoryVariables");
                    db.WithOwner().HasForeignKey("IamId");
                    db.HasKey(m => m.Id);

                    db.Property(m => m.Id)
                        .ValueGeneratedNever()
                        .HasConversion(id => id.Value, id => RotationId.Create(id))
                        .HasColumnName("RotationDependentRepositoryVariablesId")
                        .IsRequired();
                    db.Property(m => m.AccessKeyIdVariableName);
                    db.Property(m => m.AccessSecretVariableName);
                    db.Property(m => m.Environment);

                    db.HasOne<CodeRepository>().WithMany().HasForeignKey(m => m.CodeRepositoryId).IsRequired();
                });

                //ib.Navigation(i => i.RotationDependentRepositoryVariables).Metadata.SetField("_dependentRepositoryVariables");
                //ib.Navigation(i => i.RotationDependentRepositoryVariables).UsePropertyAccessMode(PropertyAccessMode.Field);
            });

        }

        private void ConfigureAWSAccountTable(EntityTypeBuilder<Account> builder)
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
