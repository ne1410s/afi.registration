namespace Afi.Registration.Api.Models
{
    /// <summary>
    /// Http response following successful registration.
    /// </summary>
    public class SuccessfulRegistrationResponse
    {
        /// <summary>
        /// Gets the customer id.
        /// </summary>
        public long CustomerId { get; init; }
    }
}
