using System;
using Afi.Registration.Api.Models;
using Afi.Registration.Domain.Services;
using FluentValidation;

namespace Afi.Registration.Api.Services
{
    /// <summary>
    /// Validates a <see cref="CustomerRegistrationRequest"/>.
    /// </summary>
    public class RegistrationRequestValidator :
        AbstractValidator<CustomerRegistrationRequest>,
        IItemValidator<CustomerRegistrationRequest>
    {
        private const string PolicyPattern = "^[A-Z]{2}-[0-9]{6}$";

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="RegistrationRequestValidator"/> class.
        /// </summary>
        public RegistrationRequestValidator()
        {
            RuleFor(x => x.PolicyReference).NotEmpty().Matches(PolicyPattern);
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
        public void ValidateItem(CustomerRegistrationRequest request)
            => this.ValidateWithDomainExceptions(request);

        private bool Be18OrOverIfSpecified(DateTime? dateOfBirth)
        {
            return dateOfBirth == null
                || dateOfBirth.Value.AddYears(18) < DateTime.Today;
        }
    }
}
