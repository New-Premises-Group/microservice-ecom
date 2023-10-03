using IW.Interfaces.Repositories;

namespace IW.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Add your Repository here
        IInventoryRepository Inventories { get; }
        ITransactionRepository Transactions { get; }
        Task<int> CompleteAsync();
    }
}
