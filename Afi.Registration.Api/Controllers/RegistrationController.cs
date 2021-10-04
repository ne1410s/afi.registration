using System.Threading.Tasks;
using Afi.Registration.Api.Models;
using Afi.Registration.Api.Services;
using Afi.Registration.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Afi.Registration.Api.Controllers
{
    /// <summary>
    /// Controller for customer registrations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IItemValidator<
            CustomerRegistrationRequest> requestValidator;
        private readonly ICustomerRequestMapper requestMapper;
        private readonly ICustomerRegistrar registrar;

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="RegistrationController"/> class.
        /// </summary>
        /// <param name="requestValidator">Customer request validator.</param>
        /// <param name="requestMapper">Customer request mapper.</param>
        /// <param name="registrar">Customer registration service.</param>
        public RegistrationController(
            IItemValidator<CustomerRegistrationRequest> requestValidator,
            ICustomerRequestMapper requestMapper,
            ICustomerRegistrar registrar)
        {
            this.requestValidator = requestValidator;
            this.requestMapper = requestMapper;
            this.registrar = registrar;
        }

        /// <summary>
        /// Registers a new customer on a policy.
        /// </summary>
        /// <param name="request">Registration request.</param>
        /// <returns>Registration response.</returns>
        [HttpPost]
        public async Task<SuccessfulRegistrationResponse> RegisterAsync(
            CustomerRegistrationRequest request)
        {
            requestValidator.ValidateItem(request);

            var mappedCustomer = requestMapper.Map(request);
            var customerId = await registrar.RegisterAsync(
                mappedCustomer,
                request.PolicyReference);

            return new()
            {
                CustomerId = customerId
            };
        }
    }
}
