using System.Linq;
using System.Threading.Tasks;
using Afi.Registration.Domain.Errors;
using Afi.Registration.Domain.Models;
using Afi.Registration.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Afi.Registration.Persistence.Repositories
{
    /// <inheritdoc cref="ICustomerRepository"/>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AfiDbContext db;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        public CustomerRepository(AfiDbContext db)
        {
            this.db = db;
        }

        /// <inheritdoc/>
        public async Task<long> AddToPolicyAsync(
            Customer customer,
            string policyReference)
        {
            var dbPolicy = db.Policies
                .SingleOrDefault(r => r.PolicyReference == policyReference)
                    ?? throw new PersistenceException("Policy not found");

            db.Customers.Add(customer);
            dbPolicy.ActivatedOn ??= System.DateTime.UtcNow;

            await db.SaveChangesAsync();
            return customer.CustomerId;
        }

        /// <inheritdoc/>
        public async Task<Customer?> FindAsync(
            Customer customer,
            string policyReference)
        {
            var hasDateOfBirth = customer.DateOfBirth != null;
            var hasEmail = !string.IsNullOrWhiteSpace(customer.Email);

            return await db.Policies
                .Include(r => r.Owner)
                .Where(r => r.PolicyReference == policyReference
                    && r.Owner != null
                    && r.Owner.Forename == customer.Forename
                    && r.Owner.Surname == customer.Surname
                    && (!hasEmail || r.Owner.Email == customer.Email)
                    && (!hasDateOfBirth
                        || r.Owner.DateOfBirth == customer.DateOfBirth))
                .Select(r => r.Owner)
                .SingleOrDefaultAsync();
        }
    }
}
