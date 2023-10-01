using IW.Interfaces;
using IW.Interfaces.Repositories;
using IW.Models;

namespace IW.Repository
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IItemRepository Items { get; }

        public IOrderRepository Orders { get; }

        public UnitOfWork(AppDbContext context,IItemRepository productRepository, IOrderRepository categoryRepository)
        {
            _context = context;
            Items= productRepository;
            Orders= categoryRepository;
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
