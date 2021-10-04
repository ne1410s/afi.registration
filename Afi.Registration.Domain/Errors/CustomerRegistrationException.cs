using System;

namespace Afi.Registration.Domain.Errors
{
    /// <summary>
    /// Represents errors occuring during customer registration.
    /// </summary>
    public class CustomerRegistrationException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="CustomerRegistrationException"/> class.
        /// </summary>
        public CustomerRegistrationException()
        { }

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="CustomerRegistrationException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public CustomerRegistrationException(string? message)
            : base(message)
        { }

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="CustomerRegistrationException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The underlying exception.</param>
        public CustomerRegistrationException(
            string? message,
            Exception? innerException)
                : base(message, innerException)
        { }
    }
}
