using Afi.Registration.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Afi.Registration.Persistence.EntityConfig
{
    /// <summary>
    /// Entity configuration for <see cref="Customer"/>.
    /// </summary>
    public class CustomerEntityConfig : IEntityTypeConfiguration<Customer>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(r => r.CustomerId);
            builder.HasIndex(r => r.Email).IsUnique();

            builder.Property(r => r.CustomerId).ValueGeneratedOnAdd();
            builder.Property(r => r.Forename).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Surname).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Email).HasMaxLength(200);

            builder.HasMany(r => r.Policies)
                .WithOne(policy => policy.Owner!)
                .HasForeignKey("OwnerId")
                .IsRequired(false);
        }
    }
}
