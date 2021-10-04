using System;

namespace Afi.Registration.Domain.Models
{
    public class Policy
    {
        public string PolicyReference { get; init; } = default!;

        public DateTime CreatedOn { get; init; } = default!;

        public DateTime? ActivatedOn { get; set; }

        public Customer? Owner { get; init; } = default!;
    }
}
