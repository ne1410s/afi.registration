using System;

namespace Afi.Registration.Domain.Models
{
    /// <summary>
    /// A policy.
    /// </summary>
    public class Policy
    {
        /// <summary>
        /// Gets the policy reference.
        /// </summary>
        public string PolicyReference { get; init; } = default!;

        /// <summary>
        /// Gets the date and time when the policy was first created.
        /// </summary>
        public DateTime CreatedOn { get; init; } = default!;

        /// <summary>
        /// Gets or sets the date and time when the policy became active.
        /// </summary>
        public DateTime? ActivatedOn { get; set; }

        /// <summary>
        /// Gets the customer that owns the policy.
        /// </summary>
        public Customer? Owner { get; init; } = default!;
    }
}
