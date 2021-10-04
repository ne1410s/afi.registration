using System.Threading.Tasks;
using Afi.Registration.Domain.Models;

namespace Afi.Registration.Domain.Repositories
{
    public interface IPolicyRepository
    {
        public Task<Policy?> FindAsync(string policyReference);
    }
}
