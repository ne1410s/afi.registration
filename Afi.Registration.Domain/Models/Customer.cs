using System;
using System.Collections.Generic;

namespace Afi.Registration.Domain.Models
{
    public class Customer
    {
        public long CustomerId { get; init; }

        public string Forename { get; set; } = default!;

        public string Surname { get; set; } = default!;

        public DateTime? DateOfBirth { get; set; }

        public string? Email { get; set; }

        public List<Policy> Policies { get; init; } = default!;
    }
}
