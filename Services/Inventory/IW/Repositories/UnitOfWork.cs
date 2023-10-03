using IW.Interfaces;
using IW.Interfaces.Repositories;
using IW.Models;

namespace IW.Repository
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IInventoryRepository Inventories { get; }

        public ITransactionRepository Transactions { get; }

        public UnitOfWork(AppDbContext context,IInventoryRepository inventoryRepository, ITransactionRepository transactionRepository)
        {
            _context = context;
            Inventories= inventoryRepository;
            Transactions= transactionRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
