using GitLabAWSKeyRotation.Domain.GitLab;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitLabAWSKeyRotation.Domain.GitLab.ValueObjects;

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
            builder.Property(m => m.Name);

        }
    }
}
