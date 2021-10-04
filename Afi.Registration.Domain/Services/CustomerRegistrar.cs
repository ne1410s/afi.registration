using System.Threading.Tasks;
using Afi.Registration.Domain.Errors;
using Afi.Registration.Domain.Models;
using Afi.Registration.Domain.Repositories;

namespace Afi.Registration.Domain.Services
{
    /// <inheritdoc cref="ICustomerRegistrar"/>
    public class CustomerRegistrar : ICustomerRegistrar
    {
        private readonly IItemValidator<Customer> customerValidator;
        private readonly ICustomerRepository customerRepo;
        private readonly IPolicyRepository policyRepo;

        /// <summary>
        /// Initialises a new instance of the <see cref="CustomerRegistrar"/>
        /// class.
        /// </summary>
        /// <param name="customerValidator">The customer validator.</param>
        /// <param name="customerRepo">The customer repo.</param>
        /// <param name="policyRepo">The policy repo.</param>
        public CustomerRegistrar(
            IItemValidator<Customer> customerValidator,
            ICustomerRepository customerRepo,
            IPolicyRepository policyRepo)
        {
            this.customerValidator = customerValidator;
            this.customerRepo = customerRepo;
            this.policyRepo = policyRepo;
        }

        /// <inheritdoc/>
        public async Task<long> RegisterAsync(
            Customer customer,
            string policyReference)
        {
            customerValidator.ValidateItem(customer);

            var policy = await policyRepo.FindAsync(policyReference, false);
            if (policy == null)
            {
                throw new PersistenceException("No matching policy found.");
            }

            var match = await customerRepo.FindAsync(customer, policyReference);
            if (match != null)
            {
                throw new PersistenceException("Customer already registered.");
            }

            return await customerRepo.AddToPolicyAsync(
                customer,
                policyReference);
        }
    }
}
