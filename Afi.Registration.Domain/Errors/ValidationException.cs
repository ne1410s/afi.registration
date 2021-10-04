using System;

namespace Afi.Registration.Domain.Errors
{
    /// <summary>
    /// Represents errors occuring during validation.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ValidationException"/>
        /// class.
        /// </summary>
        public ValidationException()
        { }

        /// <summary>
        /// Initialises a new instance of the <see cref="ValidationException"/>
        /// class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ValidationException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initialises a new instance of the <see cref="ValidationException"/>
        /// class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The underlying cause.</param>
        public ValidationException(
            string? message,
            Exception? innerException)
                : base(message, innerException)
        { }

        /// <summary>
        /// Gets any associated errors.
        /// </summary>
        public string[]? Errors { get; init; }
    }
}
