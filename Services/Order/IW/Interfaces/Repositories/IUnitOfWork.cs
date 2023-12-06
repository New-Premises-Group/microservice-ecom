using IW.Interfaces.Repositories;

namespace IW.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Add your Repository here
        IItemRepository Items { get; }
        IOrderRepository Orders { get; }
        IDiscountRepository Discounts { get; }
        Task<int> CompleteAsync();
    }
}
