using IW.Interfaces;
using IW.Interfaces.Repositories;
using IW.Models;

namespace IW.Repository
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IProductRepository Products { get; }

        public ICategoryRepository Categories { get; }

        public UnitOfWork(AppDbContext context,IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _context = context;
            Products= productRepository;
            Categories= categoryRepository;
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
