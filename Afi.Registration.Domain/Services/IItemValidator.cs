namespace Afi.Registration.Domain.Services
{
    public interface IItemValidator<TItem>
    {
        public void ValidateItem(TItem item);
    }
}
