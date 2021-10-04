using System;
using System.Collections.Generic;

namespace Afi.Registration.Domain.Models
{
    /// <summary>
    /// A customer.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets the customer id.
        /// </summary>
        public long CustomerId { get; init; }

        /// <summary>
        /// Gets or sets the forename.
        /// </summary>
        public string Forename { get; set; } = default!;

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        public string Surname { get; set; } = default!;

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets a list of customer policies.
        /// </summary>
        public List<Policy> Policies { get; init; } = default!;
    }
}
