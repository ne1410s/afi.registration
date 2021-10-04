using Afi.Registration.Domain.Errors;

namespace Afi.Registration.Domain.Services
{
    /// <summary>
    /// Validates an item.
    /// </summary>
    /// <typeparam name="TItem">The item type.</typeparam>
    public interface IItemValidator<TItem>
    {
        /// <summary>
        /// Validates an item.
        /// </summary>
        /// <param name="item">The item to validate.</param>
        /// <exception cref="ValidationException">Item is invalid.</exception>
        public void ValidateItem(TItem item);
    }
}
