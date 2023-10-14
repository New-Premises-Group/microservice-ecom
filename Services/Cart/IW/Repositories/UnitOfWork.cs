using IW.Interfaces;
using IW.Interfaces.Repositories;
using IW.Models;

namespace IW.Repository
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ICartRepository Carts { get; }

        public ICartItemRepository CartItems { get; }

        public UnitOfWork(AppDbContext context, ICartRepository inventoryRepository, ICartItemRepository transactionRepository)
        {
            _context = context;
            Carts = inventoryRepository;
            CartItems = transactionRepository;
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