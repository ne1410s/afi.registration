using System;

namespace Afi.Registration.Domain.Errors
{
    /// <summary>
    /// Represents errors occuring during customer validation.
    /// </summary>
    public class CustomerValidationException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="CustomerValidationException"/> class.
        /// </summary>
        public CustomerValidationException()
        { }

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="CustomerValidationException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public CustomerValidationException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="CustomerValidationException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The underlying cause.</param>
        public CustomerValidationException(
            string? message,
            Exception? innerException)
                : base(message, innerException)
        { }
    }
}
