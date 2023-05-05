using GitLabAWSKeyRotation.Domain.Application.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GitLabAWSKeyRotation.Infrastructure.Persistance.Configurations
{
    internal class ApplicationConfiguration : IEntityTypeConfiguration<Domain.Application.Application>
    {
        public void Configure(EntityTypeBuilder<Domain.Application.Application> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                    .ValueGeneratedNever()
                    .HasConversion(id => id.Value, id => Domain.Application.ValueObjects.ApplicationId.Create(id))
                    .HasColumnName("AccessTokenId")
                    .IsRequired();
            builder.Property(m => m.AuthKey);
        }
    }
}
