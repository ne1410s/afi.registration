using Afi.Registration.Api.Models;
using Afi.Registration.Domain.Models;

namespace Afi.Registration.Api.Services
{
    /// <inheritdoc cref="ICustomerRequestMapper"/>
    public class CustomerRequestMapper : ICustomerRequestMapper
    {
        /// <inheritdoc/>
        public Customer Map(CustomerRegistrationRequest request)
            => new()
            {
                Forename = request.Forename,
                Surname = request.Surname,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
            };
    }
}
