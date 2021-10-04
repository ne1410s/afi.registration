using System.Threading.Tasks;
using Afi.Registration.Domain.Models;

namespace Afi.Registration.Domain.Repositories
{
    /// <summary>
    /// Repository for the <see cref="Customer"/>.
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Gets a customer match on the supplied policy, or null if no match
        /// could be found.
        /// </summary>
        /// <param name="customer">The customer details to match.</param>
        /// <param name="policyReference">The policy reference.</param>
        public Task<Customer?> FindAsync(
            Customer customer,
            string policyReference);

        /// <summary>
        /// Adds a new customer to a policy. An unactivated policy is activated
        /// as part of this process.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="policyReference">The policy reference.</param>
        /// <returns>The new customer id.</returns>
        public Task<long> AddToPolicyAsync(
            Customer customer,
            string policyReference);
    }
}
