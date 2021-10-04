using System;

namespace Afi.Registration.Domain.Errors
{
    /// <summary>
    /// Represents errors occuring during data persistence.
    /// </summary>
    public class PersistenceException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PersistenceException"/>
        /// class.
        /// </summary>
        public PersistenceException()
        { }

        /// <summary>
        /// Initialises a new instance of the<see cref="PersistenceException"/>
        /// class.
        /// </summary>
        /// <param name="message">The message.</param>
        public PersistenceException(string? message) : base(message)
        { }

        /// <summary>
        /// Initialises a new instance of the <see cref="PersistenceException"/>
        /// class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The underlying cause.</param>
        public PersistenceException(
            string? message,
            Exception? innerException)
                : base(message, innerException)
        { }
    }
}
