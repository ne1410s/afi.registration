using System.Threading.Tasks;
using Afi.Registration.Domain.Errors;
using Afi.Registration.Domain.Models;

namespace Afi.Registration.Domain.Services
{
    /// <summary>
    /// Registers customers.
    /// </summary>
    public interface ICustomerRegistrar
    {
        /// <summary>
        /// Registers a new customer against an existing policy.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="policyReference">The policy reference.</param>
        /// <returns>The newly-generated customer id.</returns>
        /// <exception cref="ValidationException">Request is invalid.</exception>
        /// <exception cref="PersistenceException">Unexpected data state.</exception>
        public Task<long> RegisterAsync(
            Customer customer,
            string policyReference);
    }
}
