using Afi.Registration.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Afi.Registration.Persistence.EntityConfig
{
    /// <summary>
    /// Entity configuration for <see cref="Policy"/>.
    /// </summary>
    public class PolicyEntityConfig : IEntityTypeConfiguration<Policy>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Policy> builder)
        {
            builder.HasAlternateKey(r => r.PolicyReference);

            builder.Property<int>("PolicyId")
                .ValueGeneratedOnAdd()
                .HasAnnotation("Key", 0)
                .IsRequired();

            builder.Property(r => r.PolicyReference).HasMaxLength(16);
            builder.Property(r => r.CreatedOn).IsRequired();

            builder.HasOne(r => r.Owner)
                .WithMany(r => r!.Policies)
                .HasForeignKey("OwnerId")
                .IsRequired(false);
        }
    }
}
