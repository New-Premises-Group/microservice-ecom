using IW.Interfaces;
using IW.Interfaces.Repositories;
using IW.Models;

namespace IW.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IItemRepository Items { get; }

        public IOrderRepository Orders { get; }

        public UnitOfWork(AppDbContext context,IItemRepository itemRepository, IOrderRepository orderRepository)
        {
            _context = context;
            Items= itemRepository;
            Orders= orderRepository;
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
