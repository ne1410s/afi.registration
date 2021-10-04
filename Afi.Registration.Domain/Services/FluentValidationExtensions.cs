using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace Afi.Registration.Domain.Services
{
    /// <summary>
    /// Extensions for fluent validation.
    /// </summary>
    public static class FluentValidationExtensions
    {
        /// <summary>
        /// Validates an item; raising domain-compliant exceptions.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="item">The item.</param>
        public static void ValidateWithDomainExceptions<T>(
            this AbstractValidator<T> validator,
            T item)
        {
            ValidationResult result = validator.Validate(item);
            if (!result.IsValid)
            {
                var messages = result.Errors.Select(e => e.ErrorMessage);
                throw new Errors.ValidationException
                {
                    Errors = messages.ToArray(),
                };
            }
        }
    }
}
