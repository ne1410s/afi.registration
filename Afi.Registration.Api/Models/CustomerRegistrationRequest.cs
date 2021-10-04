using System;

namespace Afi.Registration.Api.Models
{
    /// <summary>
    /// A request for customer registration.
    /// </summary>
    public record CustomerRegistrationRequest
    {
        /// <summary>
        /// Gets the forename.
        /// </summary>
        public string Forename { get; init; } = default!;

        /// <summary>
        /// Gets the surname.
        /// </summary>
        public string Surname { get; init; } = default!;

        /// <summary>
        /// Gets the policy reference.
        /// </summary>
        public string PolicyReference { get; init; } = default!;

        /// <summary>
        /// Gets the date of birth, if provided.
        /// </summary>
        public DateTime? DateOfBirth { get; init; }

        /// <summary>
        /// Gets the email, if provided.
        /// </summary>
        public string? Email { get; init; }
    }
}
