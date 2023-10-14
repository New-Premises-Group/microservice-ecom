using IW.Interfaces.Repositories;

namespace IW.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Add your Repository here
        ICartRepository Carts { get; }

        ICartItemRepository CartItems { get; }

        Task<int> CompleteAsync();
    }
}