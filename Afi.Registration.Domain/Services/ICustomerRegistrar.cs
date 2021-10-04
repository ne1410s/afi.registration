using System.Threading.Tasks;
using Afi.Registration.Domain.Models;

namespace Afi.Registration.Domain.Services
{
    public interface ICustomerRegistrar
    {
        public Task<long> RegisterAsync(
            Customer customer,
            string policyReference);
    }
}
