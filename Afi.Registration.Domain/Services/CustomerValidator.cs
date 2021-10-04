using System;
using Afi.Registration.Domain.Models;
using FluentValidation;

namespace Afi.Registration.Domain.Services
{
    /// <summary>
    /// Validates a <see cref="Customer"/>.
    /// </summary>
    public class CustomerValidator : AbstractValidator<Customer>,
        IItemValidator<Customer>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CustomerValidator"/>
        /// class.
        /// </summary>
        public CustomerValidator()
        {
            RuleFor(x => x.Forename).NotEmpty().Length(3, 50);
            RuleFor(x => x.Surname).NotEmpty().Length(3, 50);
            RuleFor(x => x.Email).NotEmpty()
                .Unless(x => x.DateOfBirth != null)
                .WithMessage("'Email' or 'Date of Birth' are required.");
            RuleFor(x => x.DateOfBirth).NotNull()
                .Unless(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("'Email' or 'Date of Birth' are required.");
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.DateOfBirth)
                .Must(Be18OrOverIfSpecified)
                .WithMessage("Customer must be 18 or over.");
        }

        /// <inheritdoc/>
        public void ValidateItem(Customer customer)
            => this.ValidateWithDomainExceptions(customer);

        private bool Be18OrOverIfSpecified(DateTime? dateOfBirth)
        {
            return dateOfBirth == null
                || dateOfBirth.Value.AddYears(18) < DateTime.Today;
        }
    }
}
