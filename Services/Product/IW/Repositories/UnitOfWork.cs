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
        public IReviewRepository Reviews { get; }

        public UnitOfWork(
            AppDbContext context,
            IProductRepository productRepository, 
            ICategoryRepository categoryRepository,
            IReviewRepository reviewRepository)
        {
            _context = context;
            Products= productRepository;
            Categories= categoryRepository;
            Reviews= reviewRepository;
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
