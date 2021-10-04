using System.Threading.Tasks;
using Afi.Registration.Domain.Models;
using Afi.Registration.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Afi.Registration.Persistence.Repositories
{
    /// <inheritdoc cref="IPolicyRepository"/>
    public class PolicyRepository : IPolicyRepository
    {
        private readonly AfiDbContext db;

        /// <summary>
        /// Initialises a new instance of the <see cref="PolicyRepository"/>
        /// class.
        /// </summary>
        public PolicyRepository(AfiDbContext db)
        {
            this.db = db;
        }

        /// <inheritdoc/>
        public async Task<Policy?> FindAsync(
            string policyReference,
            bool? activated)
        {
            return await db.Policies
                .FirstOrDefaultAsync(
                    r => r.PolicyReference == policyReference
                    && (activated == null
                        || r.ActivatedOn.HasValue == activated));
        }
    }
}
