using IW.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace IW.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Add your Repository here
        IInventoryRepository Inventories { get; }
        ITransactionRepository Transactions { get; }
        public IDbContextTransaction StartTrasaction();
        public static void Commit(IDbContextTransaction trans)
        {
            trans.Commit();
        }
        public async static Task Rollback(IDbContextTransaction trans)
        {
            await trans.RollbackAsync();
        }
        Task<int> CompleteAsync();
    }
}
