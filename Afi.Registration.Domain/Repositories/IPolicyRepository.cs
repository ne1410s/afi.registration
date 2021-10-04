using System.Threading.Tasks;
using Afi.Registration.Domain.Models;

namespace Afi.Registration.Domain.Repositories
{
    /// <summary>
    /// Repository for the <see cref="Policy"/>.
    /// </summary>
    public interface IPolicyRepository
    {
        /// <summary>
        /// Finds a policy by its reference number and its activation status.
        /// </summary>
        /// <param name="policyReference">The policy reference.</param>
        /// <param name="activated">Whether to confine the search to activated
        /// or unactivated policies. If null, activation status is disregarded.
        /// </param>
        /// <returns>The policy, or null if none found.</returns>
        public Task<Policy?> FindAsync(string policyReference, bool? activated);
    }
}
