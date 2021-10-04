using System.Threading.Tasks;
using Afi.Registration.Domain.Models;

namespace Afi.Registration.Domain.Repositories
{
    public interface ICustomerRepository
    {
        public Task<Customer?> FindAsync(
            Customer customer,
            string policyReference);

        public Task<long> AddToPolicyAsync(
            Customer customer,
            string policyReference);
    }
}
