using Afi.Registration.Api.Models;
using Afi.Registration.Domain.Models;

namespace Afi.Registration.Api.Services
{
    /// <summary>
    /// Maps customer requests.
    /// </summary>
    public interface ICustomerRequestMapper
    {
        /// <summary>
        /// Maps a <see cref="CustomerRegistrationRequest"/> to a
        /// <see cref="Customer"/>.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Mapped customer data.</returns>
        public Customer Map(CustomerRegistrationRequest request);
    }
}
