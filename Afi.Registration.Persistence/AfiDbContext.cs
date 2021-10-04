using Afi.Registration.Domain.Models;
using Afi.Registration.Persistence.EntityConfig;
using Microsoft.EntityFrameworkCore;

namespace Afi.Registration.Persistence
{
    /// <summary>
    /// Data context for the Afi database.
    /// </summary>
    public class AfiDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AfiDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AfiDbContext(DbContextOptions<AfiDbContext> options)
            : base(options)
        { }

        /// <summary>
        /// Gets or sets the customers table.
        /// </summary>
        public DbSet<Customer> Customers { get; set; } = default!;

        /// <summary>
        /// Gets or sets the policies table.
        /// </summary>
        public DbSet<Policy> Policies { get; set; } = default!;

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerEntityConfig());
            modelBuilder.ApplyConfiguration(new PolicyEntityConfig());
        }
    }
}
